# Majority.RemittanceProvider API documentation

This repository contains the documentation for Majority.RemittanceProvider API.
created by : Thilini Kaluthanthri

#### Contents

- [Overview](#1-overview)
- [Applications](#2-Applications)
  - [Majority.RemittanceProvider.API](#21Majority.RemittanceProvider.API)
  - [Majority.RemittanceProvider.IdentityServer](#22Majority.RemittanceProvider.IdentityServer)
  - [Majority.RemittanceProvider.UnitTest](#23Majority.RemittanceProvider.UnitTest)
- [Authentication](#3-authentication)
- [Database Diagram](#4-DBDiagram)
- [Resources](#5-Resources)
  - [Get Exchange Rate](#51-Get-Exchange-Rate)
  - [Get Bank List](#52-Get-Bank-List)
  - [Get Beneficiary Name](#53-Get-Beneficiary-Name)
  - [Submit Transaction](#54Submit-Transactions)
  - [Get State List](#55-Get-State-List)
  - [Get Transaction Status](#56-Get-Transaction-Status)
  - [Get Country List](#57-Get-Country-List)
  - [Get Fees List](#58-Get-Fees-List)
- [Testing](#6-testing)
  - [Integration tests](#61-integration-tests)
  - [Unit Test](#62-Unit-Test)



## 1. Overview

Majority.RemittanceProvider API is a JSON-based general money transfer REST API.

## 2. Applications

###  2.1. Majority.RemittanceProvider.API
Technologies Used : C# , repository patteren, SQL database, Dapper, layered architecture
###  2.2. Majority.RemittanceProvider.IdentityServer

## 3. Authentication
For Authentication purpose The client will request an access token from the Identity Server using its client ID and secret and then use the token to gain access to the API

```
https://localhost:44370/connect/token?client_id={{clientId}}
    &client_secret={{client_secret}}
    &scope ={{RemittanceProviderApi.read}}
    &response_type=code
    &grant_type=client_credentials
```

With the following parameters:

| Parameter            | Type     | Required?  | Description                                                                                                                        |
| ---------------------|----------|------------|------------------------------------------------------------------------------------------------------------------------------------|
| `client_id`          | string   | required   | The clientId we will supply you that identifies your integration.                                                                  |
| `scope`              | string   | required   | The access that your integration is requesting, comma separated. Currently, there are 2 valid scope values, which are listed below.|
| `client_secret`      | string   | required   | unique Id specify only to the application and the authorization server                                                             |
| `grant_type`         | string   | required   | The field currently has only one valid value, and should be `client_credentials`.                                                                |

The following scope values are valid:

| Scope                            | Description                                                             | 
| ---------------------------------| ----------------------------------------------------------------------- | 
| RemittanceProviderApi.read       | Grants basic access to read data.                                       | 
| RemittanceProviderApi.write      | Grants basic access to write data                                       | 

sample postman request


![auth](https://user-images.githubusercontent.com/12220065/163571397-afc67307-191d-42ab-bed1-8c79f269e64b.PNG)


## 4. Database Diagram
![DBDiagram](https://user-images.githubusercontent.com/12220065/163540016-d4e5c791-1b39-4f08-badc-3489cab82b7d.PNG)


## 5. Resources
The API is RESTful and arranged around resources. All requests must be made with an Access token. All requests must be made using https.

### 5.1. Get Exchange Rate
Returns the exchange rate for the specified destination rounded to 3 decimal places.
```
POST https://localhost:44390/Transaction/get-exchange-rate
```
Where Authorization Bearer token is used for the authorized user.
Example request:

```
POST Transaction/get-exchange-rate HTTP/1.1
Host: https://localhost:44390/
Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjZBN0FCNDY3NThEMTlFN0EyOEFCMzkzQTc0QUExOEQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDk4MjUxOTAsImV4cCI6MTY0OTgyODc5MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNzAiLCJjbGllbnRfaWQiOiI0Mzg4NzI5NDQ2NjYtZWVtcHRubmxlcDJrM2Uyc2xpY2Y3MWRoM2ZrODBxMmgiLCJqdGkiOiIzMkZGOTNGQUExOEY0Q0MyQ0NDREU3QTZFOTA2RjYwQiIsImlhdCI6MTY0OTgyNTE5MCwic2NvcGUiOlsiUmVtaXR0YW5jZVByb3ZpZGVyQXBpLnJlYWQiXX0.gOtjBxB38No2rhTa-RSeMkpbwcniu0dmZT66aQmTr3XYr4HfkHr3yj6XL11FJn6ud0kXVFhmdJJu8bd8Dw1a1TNFgAhiyzkjsWFbaS0vq_M5tJkIiQ4EVLkB0eYFSclORlffip8uselh81w6sImh7HuQg7iCKASkOp2Ab_8SwcLD656Yskp1VNPn9LOZH7TghElbawPrDefXot3Qianql2V-oSLb6-jDw48kMqkY71afnc2E7q4bgY5BA028uXfr97ppEjcD5puZaRKoscciuVAoRJcMJzOoh2_pC6bJPoZLFj3biPl7Nwo01IfmPNF8tNG8CCDhc0KbYxhaXgOpSg
Content-Type: application/json
Accept: application/json
Accept-Charset: utf-8

{
	"From":"US",
	"To":"DK"
}

```
With the following fields:
| Parameter       | Type         | Required?  | Description                                                         |
| -------------   |--------------|------------|-------------------------------------------------                    |
| From            | string       | Optional   | Source Country Code                                                 |
| To              | string       | required   | Destination Country Code at the moment i assumed this is for US only|

The response is a Post object within a data envelope. Example response:

```
HTTP/1.1 201 OK
Content-Type: application/json; charset=utf-8

{
    "status": "Success",
    "httpStatusCode": 200,
    "result": {
        "sourceCountry": "US",
        "destinationCountry": "DK",
        "exchangeRate": 6.87,
        "exchangeRateToken": "DKK987965909090"
    }
}

```

Where a Post object is:

| Field         | Type         | Description                                     |
| --------------|--------------|-------------------------------------------------|
| status        | string       | Response Status                                 |
| httpStatusCode| string       | Response Status Code                            |
| result        | string       | result object                                   |

result Object 

| Field                  | Type         | Description                                                                               |
| -----------------------|--------------|-------------------------------------------------------------------------------------------|
| sourceCountry          | string       | Source Country Code                                                                       |
| destinationCountry     | string       | Destination Country Code                                                                  |
| exchangeRate           | string       | The price of a source country's money in relation to destination country's money.         |
| exchangeRateToken      | string       | exchangeRate releted unique token                                                         |


Possible errors:

| Error code           | Description                                                                                                          |
| ---------------------|----------------------------------------------------------------------------------------------------------------------|
| 440 Failed           | Required fields were invalid, not specified.                                                                         |
| 401 Unauthorized     | The access token is invalid or has been revoked.                                                                     |

sample postman request
![exchange_rate](https://user-images.githubusercontent.com/12220065/163569089-8d46495f-0d5a-4873-b7e3-d47ba97fc224.PNG)



### 5.2. Get Bank List
Returns a list of banks with their respective code.
```
POST https://localhost:44390/bank/get-bank-list
```
Where Authorization Bearer token is used for the authorized user.
Example request:

```
POST  bank/get-bank-list HTTP/1.1
Host: https://localhost:44390/
Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjZBN0FCNDY3NThEMTlFN0EyOEFCMzkzQTc0QUExOEQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDk4MjUxOTAsImV4cCI6MTY0OTgyODc5MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNzAiLCJjbGllbnRfaWQiOiI0Mzg4NzI5NDQ2NjYtZWVtcHRubmxlcDJrM2Uyc2xpY2Y3MWRoM2ZrODBxMmgiLCJqdGkiOiIzMkZGOTNGQUExOEY0Q0MyQ0NDREU3QTZFOTA2RjYwQiIsImlhdCI6MTY0OTgyNTE5MCwic2NvcGUiOlsiUmVtaXR0YW5jZVByb3ZpZGVyQXBpLnJlYWQiXX0.gOtjBxB38No2rhTa-RSeMkpbwcniu0dmZT66aQmTr3XYr4HfkHr3yj6XL11FJn6ud0kXVFhmdJJu8bd8Dw1a1TNFgAhiyzkjsWFbaS0vq_M5tJkIiQ4EVLkB0eYFSclORlffip8uselh81w6sImh7HuQg7iCKASkOp2Ab_8SwcLD656Yskp1VNPn9LOZH7TghElbawPrDefXot3Qianql2V-oSLb6-jDw48kMqkY71afnc2E7q4bgY5BA028uXfr97ppEjcD5puZaRKoscciuVAoRJcMJzOoh2_pC6bJPoZLFj3biPl7Nwo01IfmPNF8tNG8CCDhc0KbYxhaXgOpSg
Content-Type: application/json
Accept: application/json
Accept-Charset: utf-8

```

The response is a Post object within a data envelope. 
Example response:

```
HTTP/1.1 201 OK
Content-Type: application/json; charset=utf-8

{
    "status": "Success",
    "httpStatusCode": 200,
    "result": [
        {
            "name": "Wells Fargo",
            "bankCode": "WFBIUS6S"
        },
        {
            "name": "Northrim bank",
            "bankCode": "125200934"
        },
        {
            "name": "Jyske Bank",
            "bankCode": "DKW56257"
        }
    ]
}

```

Where a Post object is:

| Field         | Type         | Description                                     |
| --------------|--------------|-------------------------------------------------|
| status        | string       | Response Status                                 |
| httpStatusCode| string       | Response Status Code                            |
| result        | object array | result object                                   |

result Object 

| Field                  | Type         | Description                                                                               |
| -----------------------|--------------|-------------------------------------------------------------------------------------------|
| name                   | string       | Source Country Code                                                                       |
| bankCode               | string       | Destination Country Code                                                                  |


Possible errors:

| Error code           | Description                                                                                                          |
| ---------------------|----------------------------------------------------------------------------------------------------------------------|
| 440 Failed           | Required fields were invalid, not specified.                                                                         |
| 401 Unauthorized     | The access token is invalid or has been revoked.                                                                     |

sample postman request

![banklist](https://user-images.githubusercontent.com/12220065/163568980-2c1247ba-2e43-49e1-bf66-0b55cc8b089f.PNG)


### 5.3. Get Beneficiary Name
Returns beneficiary name associated with supplied account number.
```
POST https://localhost:44390/bank/get-beneficiary-name
```
Where Authorization Bearer token is used for the authorized user.
Example request:

```
POST bank/get-beneficiary-name HTTP/1.1
Host: https://localhost:44390/
Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjZBN0FCNDY3NThEMTlFN0EyOEFCMzkzQTc0QUExOEQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDk4MjUxOTAsImV4cCI6MTY0OTgyODc5MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNzAiLCJjbGllbnRfaWQiOiI0Mzg4NzI5NDQ2NjYtZWVtcHRubmxlcDJrM2Uyc2xpY2Y3MWRoM2ZrODBxMmgiLCJqdGkiOiIzMkZGOTNGQUExOEY0Q0MyQ0NDREU3QTZFOTA2RjYwQiIsImlhdCI6MTY0OTgyNTE5MCwic2NvcGUiOlsiUmVtaXR0YW5jZVByb3ZpZGVyQXBpLnJlYWQiXX0.gOtjBxB38No2rhTa-RSeMkpbwcniu0dmZT66aQmTr3XYr4HfkHr3yj6XL11FJn6ud0kXVFhmdJJu8bd8Dw1a1TNFgAhiyzkjsWFbaS0vq_M5tJkIiQ4EVLkB0eYFSclORlffip8uselh81w6sImh7HuQg7iCKASkOp2Ab_8SwcLD656Yskp1VNPn9LOZH7TghElbawPrDefXot3Qianql2V-oSLb6-jDw48kMqkY71afnc2E7q4bgY5BA028uXfr97ppEjcD5puZaRKoscciuVAoRJcMJzOoh2_pC6bJPoZLFj3biPl7Nwo01IfmPNF8tNG8CCDhc0KbYxhaXgOpSg
Content-Type: application/json
Accept: application/json
Accept-Charset: utf-8

{
	"AccountNumber": "2735600359",
	"BankCode": "WFBIUS6S"
}

```
With the following fields:
| Parameter       | Type         | Required?  | Description                                                         |
| -------------   |--------------|------------|-------------------------------------------------                    |
| AccountNumber   | string       | required   | Bank account number                                                 |
| BankCode        | string       | required   | unique code that is related to particular bank                      |

The response is a Post object within a data envelope. Example response:

```
HTTP/1.1 201 OK
Content-Type: application/json; charset=utf-8

{
    "status": "Success",
    "httpStatusCode": 200,
    "result": {
        "accountName": "Glinda Southgood"
    }
}

```

Where a Post object is:

| Field         | Type         | Description                                     |
| --------------|--------------|-------------------------------------------------|
| status        | string       | Response Status                                 |
| httpStatusCode| string       | Response Status Code                            |
| result        | string       | result object                                   |

result Object 

| Field                  | Type         | Description                                                                               |
| -----------------------|--------------|-------------------------------------------------------------------------------------------|
| accountName            | string       |Full name of the account holder                                                            |


Possible errors:

| Error code           | Description                                                                                                          |
| ---------------------|----------------------------------------------------------------------------------------------------------------------|
| 440 Failed           | Required fields were invalid, not specified.                                                                         |
| 401 Unauthorized     | The access token is invalid or has been revoked.                                                                     |

sample postman request

![GetBeneficiaryName](https://user-images.githubusercontent.com/12220065/163568739-1a533b2f-d1e6-4fed-923a-4defc431f096.PNG)


### 5.4. Submit Transaction

Returns a transaction ID.
```
POST https://localhost:44390/Transaction/submit-transaction
```
Where Authorization Bearer token is used for the authorized user.
Example request:

```
POST Transaction/submit-transaction HTTP/1.1
Host: https://localhost:44390/
Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjZBN0FCNDY3NThEMTlFN0EyOEFCMzkzQTc0QUExOEQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDk4MjUxOTAsImV4cCI6MTY0OTgyODc5MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNzAiLCJjbGllbnRfaWQiOiI0Mzg4NzI5NDQ2NjYtZWVtcHRubmxlcDJrM2Uyc2xpY2Y3MWRoM2ZrODBxMmgiLCJqdGkiOiIzMkZGOTNGQUExOEY0Q0MyQ0NDREU3QTZFOTA2RjYwQiIsImlhdCI6MTY0OTgyNTE5MCwic2NvcGUiOlsiUmVtaXR0YW5jZVByb3ZpZGVyQXBpLnJlYWQiXX0.gOtjBxB38No2rhTa-RSeMkpbwcniu0dmZT66aQmTr3XYr4HfkHr3yj6XL11FJn6ud0kXVFhmdJJu8bd8Dw1a1TNFgAhiyzkjsWFbaS0vq_M5tJkIiQ4EVLkB0eYFSclORlffip8uselh81w6sImh7HuQg7iCKASkOp2Ab_8SwcLD656Yskp1VNPn9LOZH7TghElbawPrDefXot3Qianql2V-oSLb6-jDw48kMqkY71afnc2E7q4bgY5BA028uXfr97ppEjcD5puZaRKoscciuVAoRJcMJzOoh2_pC6bJPoZLFj3biPl7Nwo01IfmPNF8tNG8CCDhc0KbYxhaXgOpSg
Content-Type: application/json
Accept: application/json
Accept-Charset: utf-8

{
  "senderFirstName": "Berglunds",
  "senderLastName": "snabbköp",
  "senderEmail": "snabbköp@gmail.com",
  "senderPhone": "0112222222",
  "senderAddress": "Obere Str. 57 ,Berlin",
  "senderCountry": "US",
  "senderCity": "Copenhagen",
  "sendFromState": "",
  "senderPostalCode": "299979",
  "dateOfBirth": "1967-04-14",
  "toFirstName": "Ana",
  "toLastName": "Trujillo",
  "toCountry": "DK",
  "toBankAccountName": "FixRateAccount",
  "toBankAccountNumber": "ACM0008989787",
  "toBankName": "Jyske Bank",
  "toBankCode": "DKW56257",
  "fromAmount": "3000.56",
  "exchangeRate": "",
  "fees": "25.76",
  "transactionNumber": "xdffdsdg002",
  "fromCurrency": "DKK"
}
```
With the following fields:
| Parameter             | Type         | Required?  | Description                                                         |
| -------------------   |--------------|------------|-------------------------------------------------                    |
| senderFirstName       | string       | required   | transfer customer first name                                        |
| senderLastName        | string       | required   | transfer customer last name                                         |
| senderEmail           | string       | required   | transfer customer email                                             |
| senderPhone           | string       | required   | transfer customer phone                                             |
| senderAddress         | string       | required   | transfer customer address                                           |
| senderCountry         | string       | required   | transfer customer's country                                         |
| senderCity            | string       | required   | transfer customer city                                              |
| sendFromState         | string       | required   | transfer customer state                                             |
| senderPostalCode      | string       | required   | transfer customer postal code                                       |
| dateOfBirth           | string       | required   | transfer customer date of birth                                     |
| toFirstName           | string       | required   | transferee first name                                               |
| toLastName            | string       | required   | transferee last name                                                |
| toCountry             | string       | required   | transferee country                                                  |
| toBankAccountName     | string       | required   | transferee  Bank account name                                       |
| toBankAccountNumber   | string       | required   | transferee  bank account number                                     |
| toBankName            | string       | required   | transferee bank name                                                |
| toBankCode            | string       | required   | transferee bank code                                                |
| fromAmount            | string       | required   | amount                                                              |
| exchangeRate          | string       | required   | exchange rate                                                       |
| fees                  | string       | required   | fee for exchange rate                                               |
| transactionNumber     | string       | required   | transaction number                                                  |
| fromCurrency          | string       | required   | currency type                                                       |


The response is a Post object within a data envelope. 
Example response:

```
HTTP/1.1 201 OK
Content-Type: application/json; charset=utf-8

{
    "status": "Success",
    "httpStatusCode": 201,
    "result": {
        "transactionId": "dc9d6a09-2176-4c55-a96f-cc851d16b9b2",
        "transactionStatus": "Pending payout to beneficiary"
    }
}

{
    "status": "Success",
    "httpStatusCode": 200,
    "result": {
        "transactionId": "dc9d6a09-2176-4c55-a96f-cc851d16b9b2"
    }
}

```

Where a Post object is:

| Field         | Type         | Description                                     |
| --------------|--------------|-------------------------------------------------|
| status        | string       | Response Status                                 |
| httpStatusCode| string       | Response Status Code                            |
| result        | string       | result object                                   |

result Object 

| Field                  | Type         | Description                                                                               |
| -----------------------|--------------|-------------------------------------------------------------------------------------------|
| transactionId          | string       |transaction Id                                                            |


Possible errors:

| Error code           | Description                                                                                                          |
| ---------------------|----------------------------------------------------------------------------------------------------------------------|
| 440 Failed           | Required fields were invalid, not specified.                                                                         |
| 401 Unauthorized     | The access token is invalid or has been revoked.                                                                     |

sample postman request

![Submit Transaction](https://user-images.githubusercontent.com/12220065/163568715-8e48e77d-7948-4900-874b-cfb400b3d20b.PNG)


### 5.5. Get State List
Returns a list of states in the United States.
```
POST https://localhost:44390/country/get-state-list
```
Where Authorization Bearer token is used for the authorized user.
Example request:

```
POST country/get-state-list HTTP/1.1
Host: https://localhost:44390/
Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjZBN0FCNDY3NThEMTlFN0EyOEFCMzkzQTc0QUExOEQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDk4MjUxOTAsImV4cCI6MTY0OTgyODc5MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNzAiLCJjbGllbnRfaWQiOiI0Mzg4NzI5NDQ2NjYtZWVtcHRubmxlcDJrM2Uyc2xpY2Y3MWRoM2ZrODBxMmgiLCJqdGkiOiIzMkZGOTNGQUExOEY0Q0MyQ0NDREU3QTZFOTA2RjYwQiIsImlhdCI6MTY0OTgyNTE5MCwic2NvcGUiOlsiUmVtaXR0YW5jZVByb3ZpZGVyQXBpLnJlYWQiXX0.gOtjBxB38No2rhTa-RSeMkpbwcniu0dmZT66aQmTr3XYr4HfkHr3yj6XL11FJn6ud0kXVFhmdJJu8bd8Dw1a1TNFgAhiyzkjsWFbaS0vq_M5tJkIiQ4EVLkB0eYFSclORlffip8uselh81w6sImh7HuQg7iCKASkOp2Ab_8SwcLD656Yskp1VNPn9LOZH7TghElbawPrDefXot3Qianql2V-oSLb6-jDw48kMqkY71afnc2E7q4bgY5BA028uXfr97ppEjcD5puZaRKoscciuVAoRJcMJzOoh2_pC6bJPoZLFj3biPl7Nwo01IfmPNF8tNG8CCDhc0KbYxhaXgOpSg
Content-Type: application/json
Accept: application/json
Accept-Charset: utf-8



```

The response is a Post object within a data envelope. 

Example response:

```
HTTP/1.1 201 OK
Content-Type: application/json; charset=utf-8

{
    "status": "Success",
    "httpStatusCode": 200,
    "result": [
        {
            "name": "Alabama",
            "code": "AL"
        },
        {
            "name": "Alaska",
            "code": "AK"
        },
        {
            "name": "Arizona",
            "code": "AZ"
        },
        {
            "name": "Arkansas",
            "code": "AR"
        },
        {
            "name": "California",
            "code": "CA"
        },
        {
            "name": "Colorado",
            "code": "CO"
        },
        {
            "name": "Connecticut",
            "code": "CT"
        }
       ....
    ]
}
```

Where a Post object is:

| Field         | Type         | Description                                     |
| --------------|--------------|-------------------------------------------------|
| status        | string       | Response Status                                 |
| httpStatusCode| string       | Response Status Code                            |
| result        | string       | result object                                   |

result Object 

| Field                  | Type         | Description                                                                               |
| -----------------------|--------------|-------------------------------------------------------------------------------------------|
| name                   | string       |state name                                                                                 |
| code                   | string       |postal abbreviation code                                                                   |



Possible errors:

| Error code           | Description                                                                                                          |
| ---------------------|----------------------------------------------------------------------------------------------------------------------|
| 440 Failed           | Required fields were invalid, not specified.                                                                         |
| 401 Unauthorized     | The access token is invalid or has been revoked.                                                                     |

sample postman request

![state_list](https://user-images.githubusercontent.com/12220065/163568674-852d9b13-ad85-4194-b185-cb865c3e57ef.PNG)


### 5.6. Get Transaction Status
Returns the status of a submitted transaction.
200 - Completed
201 - Pending
202 - Canceled
203 - Declined
```
POST https://localhost:44390/Transaction/get-transaction-state?transactionId=3b908a9e-9c0c-4d74-a332-bbb7690a6540
```
Where Authorization Bearer token is used for the authorized user.
Example request:

```
POST Transaction/get-transaction-state?transactionId=3b908a9e-9c0c-4d74-a332-bbb7690a6540 HTTP/1.1
Host: https://localhost:44390/
Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjZBN0FCNDY3NThEMTlFN0EyOEFCMzkzQTc0QUExOEQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDk4MjUxOTAsImV4cCI6MTY0OTgyODc5MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNzAiLCJjbGllbnRfaWQiOiI0Mzg4NzI5NDQ2NjYtZWVtcHRubmxlcDJrM2Uyc2xpY2Y3MWRoM2ZrODBxMmgiLCJqdGkiOiIzMkZGOTNGQUExOEY0Q0MyQ0NDREU3QTZFOTA2RjYwQiIsImlhdCI6MTY0OTgyNTE5MCwic2NvcGUiOlsiUmVtaXR0YW5jZVByb3ZpZGVyQXBpLnJlYWQiXX0.gOtjBxB38No2rhTa-RSeMkpbwcniu0dmZT66aQmTr3XYr4HfkHr3yj6XL11FJn6ud0kXVFhmdJJu8bd8Dw1a1TNFgAhiyzkjsWFbaS0vq_M5tJkIiQ4EVLkB0eYFSclORlffip8uselh81w6sImh7HuQg7iCKASkOp2Ab_8SwcLD656Yskp1VNPn9LOZH7TghElbawPrDefXot3Qianql2V-oSLb6-jDw48kMqkY71afnc2E7q4bgY5BA028uXfr97ppEjcD5puZaRKoscciuVAoRJcMJzOoh2_pC6bJPoZLFj3biPl7Nwo01IfmPNF8tNG8CCDhc0KbYxhaXgOpSg
Content-Type: application/json
Accept: application/json
Accept-Charset: utf-8
```

The response is a Post object within a data envelope. 

Example response:

```
HTTP/1.1 201 OK
Content-Type: application/json; charset=utf-8

{
    "status": "Success",
    "httpStatusCode": 200,
    "result": {
        "transactionId": "3b908a9e-9c0c-4d74-a332-bbb7690a6540",
        "status": "Pending"
    }
}
```


Where a Post object is:

| Field         | Type         | Description                                     |
| --------------|--------------|-------------------------------------------------|
| status        | string       | Response Status                                 |
| httpStatusCode| string       | Response Status Code                            |
| result        | string       | result object                                   |

result Object 

| Field                  | Type         | Description                                                                               |
| -----------------------|--------------|-------------------------------------------------------------------------------------------|
| transactionId          | string       |unique id returned from submit-transaction                                                 |
| status                 | string       |status of a submitted transaction.                                                         |


Possible errors:

| Error code           | Description                                                                                                          |
| ---------------------|----------------------------------------------------------------------------------------------------------------------|
| 440 Failed           | Required fields were invalid, not specified.                                                                         |
| 401 Unauthorized     | The access token is invalid or has been revoked.                                                                     |

sample postman request

![GetTrans_State](https://user-images.githubusercontent.com/12220065/163568631-e4ff811f-bc27-4baa-9f46-fa0e7062dae9.PNG)


### 5.7. Get Country List
Returns all the currently supported countries.
```
POST https://localhost:44390/country/get-country-list
```
Where Authorization Bearer token is used for the authorized user.
Example request:

```
POST country/get-country-list HTTP/1.1
Host: https://localhost:44390/
Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjZBN0FCNDY3NThEMTlFN0EyOEFCMzkzQTc0QUExOEQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDk4MjUxOTAsImV4cCI6MTY0OTgyODc5MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNzAiLCJjbGllbnRfaWQiOiI0Mzg4NzI5NDQ2NjYtZWVtcHRubmxlcDJrM2Uyc2xpY2Y3MWRoM2ZrODBxMmgiLCJqdGkiOiIzMkZGOTNGQUExOEY0Q0MyQ0NDREU3QTZFOTA2RjYwQiIsImlhdCI6MTY0OTgyNTE5MCwic2NvcGUiOlsiUmVtaXR0YW5jZVByb3ZpZGVyQXBpLnJlYWQiXX0.gOtjBxB38No2rhTa-RSeMkpbwcniu0dmZT66aQmTr3XYr4HfkHr3yj6XL11FJn6ud0kXVFhmdJJu8bd8Dw1a1TNFgAhiyzkjsWFbaS0vq_M5tJkIiQ4EVLkB0eYFSclORlffip8uselh81w6sImh7HuQg7iCKASkOp2Ab_8SwcLD656Yskp1VNPn9LOZH7TghElbawPrDefXot3Qianql2V-oSLb6-jDw48kMqkY71afnc2E7q4bgY5BA028uXfr97ppEjcD5puZaRKoscciuVAoRJcMJzOoh2_pC6bJPoZLFj3biPl7Nwo01IfmPNF8tNG8CCDhc0KbYxhaXgOpSg
Content-Type: application/json
Accept: application/json
Accept-Charset: utf-8



```
The response is a Post object within a data envelope. Example response:

```
HTTP/1.1 201 OK
Content-Type: application/json; charset=utf-8

{
    "status": "Success",
    "httpStatusCode": 200,
    "result": [
        {
            "name": "Albania",
            "code": "AL"
        },
        {
            "name": "United States of America",
            "code": "US"
        },
        {
            "name": "Denmark",
            "code": "DK"
        },
        {
            "name": "Sweden",
            "code": "SE"
        }
	.....
    ]
}

```

Where a Post object is:

| Field         | Type         | Description                                     |
| --------------|--------------|-------------------------------------------------|
| status        | string       | Response Status                                 |
| httpStatusCode| string       | Response Status Code                            |
| result        | string       | result object                                   |

result Object 

| Field                  | Type         | Description                                                                               |
| -----------------------|--------------|-------------------------------------------------------------------------------------------|
| name                   | string       | Name of country                                                                           |
| code                   | string       | ISO Code for country                                                                      |



Possible errors:

| Error code           | Description                                                                                                          |
| ---------------------|----------------------------------------------------------------------------------------------------------------------|
| 440 Failed           | Required fields were invalid, not specified.                                                                         |
| 401 Unauthorized     | The access token is invalid or has been revoked.                                                                     |

sample postman request

![countrylist](https://user-images.githubusercontent.com/12220065/163656582-5cc7246f-a132-4cb2-9b7e-88189064e606.PNG)


### 5.8. Get Fees List
Returns the fees for a selected destination.
```
POST https://localhost:44390/Transaction/get-fees-list
```
Where Authorization Bearer token is used for the authorized user.
Example request:

```
POST /Transaction/get-fees-list HTTP/1.1
Host: https://localhost:44390/
Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjZBN0FCNDY3NThEMTlFN0EyOEFCMzkzQTc0QUExOEQyIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2NDk4MjUxOTAsImV4cCI6MTY0OTgyODc5MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNzAiLCJjbGllbnRfaWQiOiI0Mzg4NzI5NDQ2NjYtZWVtcHRubmxlcDJrM2Uyc2xpY2Y3MWRoM2ZrODBxMmgiLCJqdGkiOiIzMkZGOTNGQUExOEY0Q0MyQ0NDREU3QTZFOTA2RjYwQiIsImlhdCI6MTY0OTgyNTE5MCwic2NvcGUiOlsiUmVtaXR0YW5jZVByb3ZpZGVyQXBpLnJlYWQiXX0.gOtjBxB38No2rhTa-RSeMkpbwcniu0dmZT66aQmTr3XYr4HfkHr3yj6XL11FJn6ud0kXVFhmdJJu8bd8Dw1a1TNFgAhiyzkjsWFbaS0vq_M5tJkIiQ4EVLkB0eYFSclORlffip8uselh81w6sImh7HuQg7iCKASkOp2Ab_8SwcLD656Yskp1VNPn9LOZH7TghElbawPrDefXot3Qianql2V-oSLb6-jDw48kMqkY71afnc2E7q4bgY5BA028uXfr97ppEjcD5puZaRKoscciuVAoRJcMJzOoh2_pC6bJPoZLFj3biPl7Nwo01IfmPNF8tNG8CCDhc0KbYxhaXgOpSg
Content-Type: application/json
Accept: application/json
Accept-Charset: utf-8

{
	"From":"US",
	"To":"DK"
}

```
With the following fields:
| Parameter       | Type         | Required?  | Description                                                           |
| -------------   |--------------|------------|-------------------------------------------------                      |
| From            | string       | Optional   | Source Country Code    (at the moment i assumed this is for US only)  |
| To              | string       | required   | Destination Country Code                                              |

The response is a Post object within a data envelope. Example response:

```
HTTP/1.1 201 OK
Content-Type: application/json; charset=utf-8

{
    "status": "Success",
    "httpStatusCode": 200,
    "result": [
        {
            "amount": "0.00 - 199.99",
            "fee": 0.2
        },
        {
            "amount": "200.00 - 399.99",
            "fee": 0.5
        },
        {
            "amount": "400.00 - 599.99",
            "fee": 0.79
        }
    ]
}

```

Where a Post object is:

| Field         | Type         | Description                                     |
| --------------|--------------|-------------------------------------------------|
| status        | string       | Response Status                                 |
| httpStatusCode| string       | Response Status Code                            |
| result        | string       | result object                                   |

result Object 

| Field                  | Type         | Description                                                                               |
| -----------------------|--------------|-------------------------------------------------------------------------------------------|
| amount                 | string       | Range of USD amount                                                                       |
| fee                    | decimal      | fee of exchange within the money range                                                    |


Possible errors:

| Error code           | Description                                                                                                          |
| ---------------------|----------------------------------------------------------------------------------------------------------------------|
| 440 Failed           | Required fields were invalid, not specified.                                                                         |
| 401 Unauthorized     | The access token is invalid or has been revoked.                                                                     |

sample postman request

![Getfee_list](https://user-images.githubusercontent.com/12220065/163568428-4397e2f4-c4b8-47f1-afbf-cd660072ce1b.PNG)



## 6. Testing
I do not included sandbox environment yet.

###  6.1. Integration Tests

Included few xunit intergration test for token validation please refer the below repository for access intergartion tests

https://github.com/TKaluthanthri/Majority.RemittanceProvider.IntegrationTest

these are the test cases i've implemented 

![testcases](https://user-images.githubusercontent.com/12220065/163662791-f67456f2-6bf3-4c3f-a8fb-792fb217e699.PNG)

###  6.1. Unit Tests

unit test done for some controller methods

![unit test](https://user-images.githubusercontent.com/12220065/163695247-3a899e6e-5133-4bab-8b53-a924672d6b81.PNG)

