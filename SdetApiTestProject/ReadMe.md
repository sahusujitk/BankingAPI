Below test Cases are written to cover Create Account, Check or Get the account, Deposit in the Account, withdraw from the account and Delete the account.

# Create an Account
Create Account with 100 i.e. Valid amount 
Create Account with 10 i.e. Invalid amount
Create Account with -10 i.e. Invalid amount
Create Account with 10001 i.e. Invalid amount 


# Delete an Account
Delete an account i.e. Valid Account
Delete an account which doesn’t exist i.e. Invalid Account


# Deposit in the Account
Deposit valid amount to an existing account
Deposit amount greater than 10000
Deposit Negative amount which causes the amount to go below 90%


# Withdraw from the Account
Withdraw valid amount
Withdraw valid amount not exceeding 90% of the funds
Withdraw amount exceeding 90% of the funds 


# Check or Get the account(s)
Check all the accounts
Check a specific account
Check a non-existing account
Check Get All accounts when no accounts exist


# Bonus Points

-What tests would you implement, in case that this API uses a real database?

When the API uses real database it is still better to test the API itself and avoid directly querying the database for testing purposes. This helps to make the tests faster and more isolated. For API testing we can do functional testing and Nonfunctional testing.

But if we still need to test database we can do Integration Testing which interacts with database. We can check CRUD operations ensuring the data is correctly stored, retrieved, Updated and deleted.
We should utilize mock databases for fast and isolated tests, focusing on testing the behavior of individual APIs. Then, incorporate real databases for integration tests or end-to-end tests, where you validate the interactions between your API and the actual database.


-  Describe the test strategy (test types, test levels etc.) in case this would be a live product, used by a large number of users. Assume that the team wants to deploy quickly - multiple times a day, and that the business is willing to invest into good testing strategy.

The test types should be Functional Testing, Nonfunctional testing, Security Testing, Performance testing.

Test levels should be Unit Testing of the APIs, API testing focusing on interactions between different API endpoints, Integration testing to ensure API is integrating seamlessly with other systems, System testing to test the entire system including the API and its downstream applications.  

For multiple deployments in a day need to focus more on automation wherever possible and integrating the automated tests in CI/CD pipeline so that build, test and deployment process is automated. 

We can also set dedicated environments for development, testing, staging and production to isolate testing activity. 

We can utilize parallel testing to execute the tests concurrently which will help rapid deployments.
  

- Explain how would you approach testing the performance of this API, assuming such requirements exist?

To test the performance of the API need to consider below points

Need to understand first the performance requirements i.e. response time, throughput, scalability and resource usage limits.

Create the test scenarios and select the testing tool for the tests.

Execute the test and analyze results.

Based on analysis of results, optimize the API implementation or infrastructure to address any issues identified during testing. 

Document the performance testing process and continuously monitor to improve the performance of API.   


- Explain how would you approach testing the security of this API, assuming such requirements exist?

Need to understand first the security requirements specified for the API which may include Authentication mechanisms, Authorization policies, Encryption Standards, data integrity checks and compliance with regulatory standards as GDPR etc.

Develop a security testing plan which outlines the scope, objectives, methodologies and tools to be used for testing the API’s security aspects. The plan should over penetration testing, vulnerability assessment and security code review.



- Explain how would the test infrastructure look like?

Should implement CI/CD pipelines for automated deployment.
User version control management toll like GIT.
Use tools like Selenium for UI, Postman or Rest Assured for API and JMeter for performance testing.


- Explain what tools for test management would you use?

Should use Azure DevOps, JIRA, TestRail etc. for managing test cases, tracking defects and generating reports.

- Explain how would you manage a situation where this API exists, but there are no written requirements for it (e.g. in case of legacy projects and apps).

Following approaches can be considered

Reverse Engineering
Code Review
Manual testing
Collaborate with the developers and stakeholders who have the knowledge of the API


 


