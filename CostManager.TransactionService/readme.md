In Future:
    1) Add powershell script to automize work from step 1.1-1.3 for local devs
    2) Add powershell script to wrap docker compose commands from step 1.4 

Using:
    1) For local run preferable way is running application through the Docker:
        1.1) Create your own Secret in Application registration service principle for local dev
        1.2) Run next commands from the CostManager.TransactionService.API folder to add necessary secrets 
            (https://dev.to/gkarwchan/using-secret-manager-for-configuration-in-net-2pi2)

            dotnet user-secrets init (check that SECRETS_FOLDER_NAME of your %APPDATA%\Roaming\Microsoft\UserSecrets\<user_secrets_id> folder and SECRETS_FOLDER_NAME in CostManager.TransactionService.API.csproj <UserSecretsId> tag are the same)
            dotnet user-secrets set "AppSettings:app-registration-id" "{Application registration service principle ID}"
            dotnet user-secrets set "AppSettings:azure-tenant-id" "{Azure Tenant ID}"
            dotnet user-secrets set "AppSettings:app-registration-secret" "{Secret value from the step 1}"
            
        Check that values in secrets.json file equal to the provided on this step

        1.3) Update docker-compose.yml file and add .env file to the CostManager.TransactionService root folder 
            (this step is for volume mapping from docker container on your own secrets folder):

            USER_SECRETS_ID="{SECRETS_FOLDER_NAME}"
            DOTNET_USER_SECRETS="{/root/.microsoft/usersecrets/SECRETS_FOLDER_NAME}"

        1.4) Run application by command:
            docker-compose up -d (-d means detach mode when you can attach terminal to the docker container for interaction e.g. via bash)
            docker-compose down (to stop and remove docker container)
            docker exec -it {SERVICE_NAME_FROM_DOCKER_COMPOSE_FILE} sh (attach terminal to the container for interaction)
            ctrl + z (to exit attached terminal)
            docker logs -f {SERVICE_NAME_FROM_DOCKER_COMPOSE_FILE} (check container logs)
            ctrl + c (exit container logs)