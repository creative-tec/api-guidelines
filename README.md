# Creative Technologies RESTful API Guidelines

## Table of Contents

1. [Introduction](#Introduction)
2. [Resources](#Resources)
2. [HTTP Verbs](#HTTP-Verbs)

## Introduction

The Creative Technologies RESTful API Guidelines provide guidance for development teams to ensure all APIs developed have a consistent design and behaviour.  Alongside this document there is a demo project demonstrating how our implementation of <a target="_blank" href="https://en.wikipedia.org/wiki/Domain-driven_design">Domain Driven Design</a> works with the guidelines.

The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL", "NOT", "SHOULD", "SHOULD NOT", "RECOMMENDED",  "MAY", and "OPTIONAL" in this document are to be interpreted as described in <a target="_blank" href="https://www.ietf.org/rfc/rfc2119.txt">RFC 2119</a> but will use **lowercase bold** text instead of capitalized text.


## Resources

Historically APIs were RPC based - GetAllAccounts(), GetAccount(123).  A RESTful API moves away from this by modelling APIs around resources.   The API **must** be based on well designed resources and use HTTP Verbs correctly to conform to a <a target="_blank" href="https://martinfowler.com/articles/richardsonMaturityModel.html">Richardson Maturity Model</a> Level 2 API.

The API **must** Use pluralised nouns, kebab-case (lowercase words separated with hyphens):
```
https://api.creative-technologies.co.uk/accounts

https://api.creative-technologies.co.uk/account-statuses
```

Nest sub resources-
```
https://api.creative-technologies.co.uk/accounts/123/transactions

https://api.creative-technologies.co.uk/accounts/123/transactions/456
```

Resources will usually map to the aggregate root from Domain Driven Design with their child entities as sub-resources.  


## HTTP Verbs

The API **must** use the HTTP verbs GET, POST, PUT, DELETE to provide operations on the resources. 

The API **must not** use the PATCH verb as this would allow entities to be updated in an invalid state.  

| Resource      | GET                   | POST              | PUT                       | DELETE             | 
| ------------- |---------------------- | ------------------| ------------------------- | ------------------ | 
| /accounts      | Gets List of Accounts  | Creates Account    | Updates Batch of Accounts  | Error              |
| /accounts/123  | Gets Account 123       | Error             | Updates Account 123        | Deletes Account 123 |

### GET

All GET request **must** be <a target="_blank" href="http://restcookbook.com/HTTP%20Methods/idempotency/">idempotent and safe</a>.  

```
GET /accounts HTTP/1.1
Host: localhost:5000

HTTP/1.1 200 OK
Content-Type: application/json

[
  {
    "id": "00000000-0000-0000-0000-000000000001",
    ...
  },
  {
    "id": "00000000-0000-0000-0000-000000000002",
    ...
  }
]
```

The API **should** use GUID's as the resource identifiers instead of an integer which would typically come from an auto incrementing column in a database.  Using GUIDs will hide the internal implementation detail and allow the undelying data store to change over time without breaking all of the clients consuming the API.

```
GET /accounts/00000000-0000-0000-0000-000000000001 HTTP/1.1
Host: localhost:5000

HTTP/1.1 200 OK
Content-Type: application/json

{
  "id": "00000000-0000-0000-0000-000000000001",
  ...
}
```
