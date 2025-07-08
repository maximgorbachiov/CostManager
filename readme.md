In Future:
    1) Make all the services totally async on create, update, delete operations. E.g. when we create new transaction with new category we should:
      1.1 - Create on api gate side guid for new category and add it to the new transaction.
      1.2 - Create two messages with new category and new transaction creations for CategoryService and TransactionService.

Daily plan:
  1) Add to CategoryService all the left functions triggered by message queues (update, delete)
    1.1) Test them locally by Azurite and Azure Storage Explorer
    1.2) Publish app to Azure so the environment api can be tested
    1.3) Add role If there will be needed any Azure Storage RBAC role for CategoryService app to read/write messages from/to queues
  2) Add triggering by message queues for TransactionsService api (create, update, delete)
    2.1) Test them locally by Azurite and Azure Storage Explorer
    2.2) Publish app to Azure so the environment api can be tested
    2.3) Add role If there will be needed any Azure Storage RBAC role for TransactionsService app to read/write messages from/to queues
  3) Add API app (CostManagerApiService based on App Service) that will be called from client and will send messages to CategoryService and TransactionsService.
    3.1) Test them locally by Azurite and Azure Storage Explorer
    3.2) Add to GitHub Actions deploying pipeline (reuse existing GitHub federated identity for deployment to Azure)
    3.3) Publish app to Azure so the environment api can be tested
    3.4) Add role If there will be needed any Azure Storage RBAC role for CostManagerApiService app to write/read messages from/to queues
    3.5) Setup DNS name.
    3.6) Setup SSL Certificate.
  4) Setup Authentification and Authorization (prefered two-factored) for the API application and base (potentially token) authentification for core services. Read
    about Authentification and Authorization on Microservices architecture.
  5) Move all infrastructure to the IoC model (Terraform).
    4.1) Create first script for TransactionsService creation.
    4.2) Create Identity for Terraform with ability to create and delete resources
    4.3) Create multiple scripts for creation, deletion of resources separately
  6) Add WidgetService ...............................
  OPTIONALY 7) Hide all the core services (except CostManagerApiService) in the virtual network with Private DNS names for the core services.