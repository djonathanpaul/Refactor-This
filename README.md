# Refactoring Assessment

Summary of what has been changed in the project


1. Deleted the Helper class. 
2. Deleted Account and Transaction classes under Models folder.

Reason for performing Steps 1 and 2 - 
I wanted to make use of entity framework to genertate the model classes. It helps in performing CRUD operations with ease rather than writing sql queries.
Reduces the effort in opening and closing connections in code. Most importantly, improves readability and can make use of pre-defined methods.


3. Added an ADO.NET Entity Data Model titled "RefractorContext" under Models folder. I have selected the "Code First From Database" option for generating this model.
   Named the connection string for this project as "RefractorContext". 
4. The above step has helped in generating the Account and Transaction Model Class. Also the ConnectionString was added in the webconfi file. 
    
5. Since I am making use of Entity Framework, I have rewritten all the action methods under "TransactionController" and "AccountController".
6. I made use of RoutePrefix attribute to ensure code-reusability.
7. Ensured that the right status code is being returned based on the logic of each function. 
	For example, in the case of Account ID not found, ensured that a status of 404 is being returned. 
	When a new Account is being addedd, ensured that a status of 201 is being returned.
	Made use of Try-Catch block so as to ensure a bad request has always been captured by the application.
8. For the AddTransaction action method in TranscationController class. 
   I made use of DBContextTransaction in order to ensure the atomicity of the database operations being carried out in a transaction.Imported System.Data.Entity to achieve the logic i have implemented.
9. Removed the Private Get() that reurns an Account object.Do not require since I am making use of enity framework that takes care of what this fucntion is achieving.



The api is composed of the following endpoints:

| Verb     | Path                                   | Description
|----------|----------------------------------------|--------------------------------------------------------
| `GET`    | `/api/Accounts`                        | Gets the list of all accounts
| `GET`    | `/api/Accounts/{id:guid}`              | Gets an account by the specified id
| `POST`   | `/api/Accounts`                        | Creates a new account
| `PUT`    | `/api/Accounts/{id:guid}`              | Updates an account
| `DELETE` | `/api/Accounts/{id:guid}`              | Deletes an account
| `GET`    | `/api/Accounts/{id:guid}/Transactions` | Gets the list of transactions for an account
| `POST`   | `/api/Accounts/{id:guid}/Transactions` | Adds a transaction to an account, and updates the amount of money in the account
