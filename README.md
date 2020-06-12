# Simple RESTAPI

- ASP .Net Core 3.1 API
- Docker
- EntityFramework MySql

## Setup

You must have Docker-Compose.

## Routes

### Get - [GET] /api/product
**You get:** List with all products.
**Request:**
**Successful Response:**
```json
[
    {
        "id": 2,
        "size": 3,
        "flavor": 2,
        "custom1": true,
        "custom2": true,
        "custom3": false,
        "time": 0,
        "timeNoCustom": 0,
        "timeOnlyCustom": 0,
        "value": 3
    }
]
```

### Get - [GET] /api/product/:id
**You send:**  Product id
**You get:** Json with information about the product.
**Request:** GET /api/product/2
**Successful Response:**
```json
[
    {
        "id": 2,
        "size": 3,
        "flavor": 2,
        "custom1": true,
        "custom2": true,
        "custom3": false,
        "time": 0,
        "timeNoCustom": 0,
        "timeOnlyCustom": 0,
        "value": 3
    }
]
```
**Failed Response:** NotFound

### Post - [POST] /api/product
**You send:**  Your form data. Fields: flavor and size. Optionals: custom1, custom2, custom3 fields.
**You get:** Feedback about insert new entry to the database
**Request:**
```json
{
    "query": [
        {
            "key": "flavor",
            "value": "3"
        },
        {
            "key": "size",
            "value": "2"
        }
    ]
}
```

**Successful Response:**
```json
{
    "urlHelper": null,
    "actionName": "Get",
    "controllerName": null,
    "routeValues": {
        "id": 2
    },
    "value": {
        "id": 2,
        "size": 2,
        "flavor": 3,
        "custom1": false,
        "custom2": false,
        "custom3": false,
        "time": 12,
        "timeNoCustom": 7,
        "value": 13
    },
    "formatters": [],
    "contentTypes": [],
    "declaredType": null,
    "statusCode": 201
}
```
**Failed Response:**
```json
{
    "Size": [
        "The value '6' is invalid."
    ],
    "Flavor": []
}
```

### PUT - [PUT] /api/product/:id
**You send:**  Your form data with an id. Fields: flavor and size. Optionals: custom1, custom2, custom3 fields.
**You get:** Feedback about editing an entry
**Request:** PUT /api/product/2
```json
{
    "query": [
        {
            "key": "flavor",
            "value": "3"
        },
        {
            "key": "size",
            "value": "2"
        },
        {
            "key": "custom1",
            "value": "true"
        },
        {
            "key": "custom2",
            "value": "false"
        }
    ]
}
```

**Successful Response:**
```json
{
    "urlHelper": null,
    "actionName": "Get",
    "controllerName": null,
    "routeValues": {
        "id": 2
    },
    "value": {
        "id": 2,
        "size": 2,
        "flavor": 3,
        "custom1": true,
        "custom2": false,
        "custom3": false,
        "time": 12,
        "timeNoCustom": 7,
        "value": 13
    },
    "formatters": [],
    "contentTypes": [],
    "declaredType": null,
    "statusCode": 201
}
```
**Failed Response:** NotFound, BadRequest.
```json
{
    "id": [],
    "Size": [],
    "Flavor": [],
    "Custom1": [
        "The value 'asd' is not valid for Custom1."
    ],
    "Custom2": []
}
```

