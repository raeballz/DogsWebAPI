# Wecome to the Rae Dog Rest API And Client

# Live Instances

 - ### ReactdotNET web client:  https://raedogwebapp.azurewebsites.net/
 - ### REST API:  https://raedogrestapi.azurewebsites.net/api/dogbreed

# Functionality

REST API
| HTTP Function | URL | Description | Successful Response Body Example | Response Codes |
|--|--|--|--|--|
| GET | /api/dogbreed/ |Fetches a Json payload of all Dog Breeds | `[ { "dogBreedItemId": 1, "breedName": "breed1","subBreeds": []}, { "dogBreedItemId": 2, "breedName": "breed2","subBreeds": [ { "parentBreedId": 14, "dogSubBreedId": 1, "subBreedName": "boston"}]} ]` | STATUS: 200, STATUS: 404  
| GET| /api/dogbreed/{id}| Fetches a Json payload of a specified dog breed by ID | `{ "dogBreedItemId": 1, "breedName": "breed1","subBreeds": []}` | STATUS: 200, STATUS: 404  
| GET | /api/dogbreed/{id}/subbreed |Fetches a Json payload of all sub-breeds of a breed by ID | `[{ "parentBreedId": 14,"dogSubBreedId": 1,"subBreedName": "boston"},{"parentBreedId":14,"dogSubBreedId": 2,"subBreedName": "french"}]` | STATUS: 200, STATUS: 404  
| GET | /api/dogbreed/{id}/subbreed/{subBreedId} | Fetches a Json payload of a sub-breed by ID  from a breed, again selected by ID | `{ "parentBreedId": 14, "dogSubBreedId": 1, "subBreedName": "boston" }` | STATUS: 200, STATUS: 404  
|DELETE| /api/dogbreed/{id} | Deletes a breed and all sub-breeds of breed by ID. | `Example delete` | STATUS: 200, STATUS: 404  
|DELETE| /api/dogbreed/{id}/subbreed/{subBreedId} | Deletes a sub-breed by Id within a breed by ID | `Example delete` | STATUS: 200, STATUS: 404  
|POST| /api/dogbreed/ | Posts a new dog breed, generates an ID, returns location in header | `{"dogBreedItemId": 82, "breedName": "new breed","subBreeds": [] }` | STATUS: 201, STATUS: 404, STATUS 400, STATUS: 422
|POST| /api/dogbreed/{id}/subbreed | Post a new sub-breed, generates an ID, links it to parent, returns location of resource in header | `{ "parentBreedId": 67, "dogSubBreedId": 66,"subBreedName": "Tastyyy"}` | STATUS: 201, STATUS: 404, STATUS 400, STATUS: 422 
|PUT| /api/dogbreed/{id} | Updates an existing breed resource | {"dogBreedItemId":1,"breedName":"affenpinscherrrrr","subBreeds": []}
