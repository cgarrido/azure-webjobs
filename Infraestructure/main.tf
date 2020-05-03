provider "azurerm" {
  version = "= 1.31.0"
}
resource "azurerm_resource_group" "DotnettersResourceGroup" {
  name     = "${var.DotnettersPrefix}-${var.DotnettersEnv}"
  location = "West US"

  tags = {
    environment = "Test"
  }
}

#Service Plan
resource "azurerm_app_service_plan" "DotnettersServicePlan" {
  name                = "${var.DotnettersPrefix}-${var.DotnettersEnv}-ServicePlan"
  location            = azurerm_resource_group.DotnettersResourceGroup.location
  resource_group_name = azurerm_resource_group.DotnettersResourceGroup.name
  #Para poder utilizar AlwaysOn en el AppService, mínimo necesitamos una Standard
  sku {
    tier = "Standard"
    size = "S1"
  }
}

#Application Insights
resource "azurerm_application_insights" "DotnettersApplicationInsights" {
  name                = "${lower(var.DotnettersPrefix)}${lower(var.DotnettersEnv)}-appinsights"
  location            = azurerm_resource_group.DotnettersResourceGroup.location
  resource_group_name = azurerm_resource_group.DotnettersResourceGroup.name
  application_type    = "Web"
}

#Storage
resource "azurerm_storage_account" "DotnettersStorage" {
  name                     = "${lower(var.DotnettersPrefix)}${lower(var.DotnettersEnv)}storage"
  resource_group_name      = azurerm_resource_group.DotnettersResourceGroup.name
  location                 = azurerm_resource_group.DotnettersResourceGroup.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

#Storage.Queue [email-queue]
resource "azurerm_storage_queue" "GHStorageQueue" {
  name                 = "email-queue"
  resource_group_name  = azurerm_resource_group.DotnettersResourceGroup.name
  storage_account_name = azurerm_storage_account.DotnettersStorage.name
}

#App Service
resource "azurerm_app_service" "DotnettersAppService" {
  name                = "${var.DotnettersPrefix}-${var.DotnettersEnv}-appservice"
  location            = azurerm_resource_group.DotnettersResourceGroup.location
  resource_group_name = azurerm_resource_group.DotnettersResourceGroup.name
  app_service_plan_id = azurerm_app_service_plan.DotnettersServicePlan.id
  site_config {
    dotnet_framework_version = "v4.0"
    always_on                = true #Necesario para que el webjob esté activo
  }
  app_settings = {
    #Clave de Application Insights
    "APPINSIGHTS_INSTRUMENTATIONKEY" = "${azurerm_application_insights.DotnettersApplicationInsights.instrumentation_key}",
    "SendGrid:APIKey" = "${var.DotnettersSendGridAPIKey}",
    "SendGrid:EmailFrom" ="welcome@welcome.com",
    "SendGrid:NameFrom" ="Dotnetters WebJobs Demo"
  }
  connection_string {
    name  = "AzureWebJobsDashboard"
    type  = "Custom"
    value = azurerm_storage_account.DotnettersStorage.primary_connection_string
  }
  connection_string {
    name  = "AzureWebJobsStorage"
    type  = "Custom"
    value = azurerm_storage_account.DotnettersStorage.primary_connection_string
  }
}
