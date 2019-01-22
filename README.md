---


---

<h1 id="wecome-to-the-rae-dog-rest-api-and-client">Wecome to the Rae Dog Rest API And Client</h1>
<h1 id="live-instances">Live Instances</h1>
<ul>
<li>
<h3 id="reactdotnet-web-client--httpsraedogwebapp.azurewebsites.net">ReactdotNET web client:  <a href="https://raedogwebapp.azurewebsites.net/">https://raedogwebapp.azurewebsites.net/</a></h3>
</li>
<li>
<h3 id="rest-api--httpsraedogrestapi.azurewebsites.netapidogbreed">REST API:  <a href="https://raedogrestapi.azurewebsites.net/api/dogbreed">https://raedogrestapi.azurewebsites.net/api/dogbreed</a></h3>
</li>
</ul>
<h1 id="functionality">Functionality</h1>
<h2 id="rest-api">REST API</h2>

<table>
<thead>
<tr>
<th>HTTP Function</th>
<th>URL</th>
<th>Description</th>
<th>Successful Response Body Example</th>
<th>Response Codes</th>
</tr>
</thead>
<tbody>
<tr>
<td>GET</td>
<td>/api/dogbreed/</td>
<td>Fetches a Json payload of all Dog Breeds</td>
<td><code>[ { "dogBreedItemId": 1, "breedName": "breed1","subBreeds": []}, { "dogBreedItemId": 2, "breedName": "breed2","subBreeds": [ { "parentBreedId": 14, "dogSubBreedId": 1, "subBreedName": "boston"}]} ]</code></td>
<td>STATUS: 200, STATUS: 404</td>
</tr>
<tr>
<td>GET</td>
<td>/api/dogbreed/{id}</td>
<td>Fetches a Json payload of a specified dog breed by ID</td>
<td><code>{ "dogBreedItemId": 1, "breedName": "breed1","subBreeds": []}</code></td>
<td>STATUS: 200, STATUS: 404</td>
</tr>
<tr>
<td>GET</td>
<td>/api/dogbreed/{id}/subbreed</td>
<td>Fetches a Json payload of all sub-breeds of a breed by ID</td>
<td><code>[{ "parentBreedId": 14,"dogSubBreedId": 1,"subBreedName": "boston"},{"parentBreedId":14,"dogSubBreedId": 2,"subBreedName": "french"}]</code></td>
<td>STATUS: 200, STATUS: 404</td>
</tr>
<tr>
<td>GET</td>
<td>/api/dogbreed/{id}/subbreed/{subBreedId}</td>
<td>Fetches a Json payload of a sub-breed by ID  from a breed, again selected by ID</td>
<td><code>{ "parentBreedId": 14, "dogSubBreedId": 1, "subBreedName": "boston" }</code></td>
<td>STATUS: 200, STATUS: 404</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/dogbreed/{id}</td>
<td>Deletes a breed and all sub-breeds of breed by ID.</td>
<td><code>{ "parentBreedId": 80, "dogSubBreedId": 65, "subBreedName": "irish" }</code></td>
<td>STATUS: 200, STATUS: 404</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/dogbreed/{id}/subbreed/{subBreedId}</td>
<td>Deletes a sub-breed by Id within a breed by ID</td>
<td><code>{ "dogBreedItemId": 80, "breedName": "wolfhound", "subBreeds": null}</code></td>
<td>STATUS: 200, STATUS: 404</td>
</tr>
<tr>
<td>POST</td>
<td>/api/dogbreed/</td>
<td>Posts a new dog breed, generates an ID, returns location in header</td>
<td><code>{"dogBreedItemId": 82, "breedName": "new breed","subBreeds": [] }</code></td>
<td>STATUS: 201, STATUS: 404, STATUS 400, STATUS: 422</td>
</tr>
<tr>
<td>POST</td>
<td>/api/dogbreed/{id}/subbreed</td>
<td>Post a new sub-breed, generates an ID, links it to parent, returns location of resource in header</td>
<td><code>{ "parentBreedId": 67, "dogSubBreedId": 66,"subBreedName": "Tastyyy"}</code></td>
<td>STATUS: 201, STATUS: 404, STATUS 400, STATUS: 422</td>
</tr>
<tr>
<td>PUT</td>
<td>/api/dogbreed/{id}</td>
<td>Updates an existing breed resource</td>
<td><code>{"dogBreedItemId":1,"breedName":"affenpinscherrrrr","subBreeds": []}</code></td>
<td>STATUS: 200, STATUS 400</td>
</tr>
</tbody>
</table><h2 id="rest-server-functionality">REST Server Functionality</h2>
<ul>
<li>Built on: <a href="http://ASP.NET">ASP.NET</a> Core MVC</li>
<li>Hosted separately from React webapp. API should be able to continue service other applications whilst we take down the react webapp and update it.</li>
<li>In-memory Database: Generated using EntityFramework. Used rather than an in memory data structure/reading a text file for scalability. Builds separate DB sets for breeds and sub-breeds, creates keys to link them together. ID’s are longs rather than using the breed name as a unique ID for faster searches. FUTURE IMPROVEMENT: In-memory database should be easy enough to convert to a db schema, and store the records outside the service.</li>
<li>Mutates data from dogs.json format into our more scaleable payload style.</li>
<li>Data key overlaps: Server enforces on POST that two breeds can not be the same name. This also allows for multiple sub-breeds of the same name, but not within same breed.  EG “American” is a sub-breed of breeds “Bull Dog”  and “Fox Hound”, but we could not have two “Fox Hound” breeds.</li>
<li>Malformed data correction: Creates empty sub-breed arrays on POST of a breed object, adds an ID if not provided. Replaces ID with a correct ID if the ID intersects with another database record. Updates “parentBreedId” on created sub-breeds to ensure the DB populates and links records correctly.</li>
<li>Deployed via click-once to Azure Cloud.</li>
</ul>
<h2 id="react-dotnet--webapp-functionality">React dotNet  WebApp Functionality</h2>
<ul>
<li>Uses react router for navigation, will allow the use of “back button” within your browser correctly.</li>
<li>Can Reset DB to original json file provided.</li>
<li>Separated from REST client, can switch out URLs to point to different data sources.</li>
<li>Gives UI to see the DB structure, nicely formatted as a JSON payload.</li>
<li>Gives UI to see all dog breeds and sub-breeds in a table form. UI buttons to Add and Delete breeds and sub-breeds. Gives modal popups with a form for easy user input.</li>
<li>Page stays in sync with server on data update via React states.</li>
<li>Links to additional information.</li>
<li>Deployed via click-once to Azure Cloud.</li>
</ul>
<h3 id="data-transformation">Data Transformation</h3>
<h4 id="before">Before</h4>
<p><code>{ "affenpinscher": [], "african": [], "airedale": [], "akita": [], "appenzeller": [], "basenji": [], "beagle": [], "bluetick": [], "borzoi": [], "bouvier": [], "boxer": [], "brabancon": [], "briard": [], "bulldog": ["boston", "french"], "bullterrier": ["staffordshire"], "cairn": [], "chihuahua": [], "chow": [], "clumber": [], "collie": ["border"], "coonhound": [], "corgi": ["cardigan"], "dachshund": [], "dane": ["great"], "deerhound": ["scottish"], "dhole": [], "dingo": [], "doberman": [], "elkhound": ["norwegian"], "entlebucher": [], "eskimo": [], "germanshepherd": [], "greyhound": ["italian"], "groenendael": [], "hound": ["Ibizan", "afghan", "basset", "blood", "english", "walker"], "husky": [], "keeshond": [], "kelpie": [], "komondor": [], "kuvasz": [], "labrador": [], "leonberg": [], "lhasa": [], "malamute": [], "malinois": [], "maltese": [], "mastiff": ["bull", "tibetan"], "mexicanhairless": [], "mountain": ["bernese", "swiss"], "newfoundland": [], "otterhound": [], "papillon": [], "pekinese": [], "pembroke": [], "pinscher": ["miniature"], "pointer": ["german"], "pomeranian": [], "poodle": ["miniature", "standard", "toy"], "pug": [], "pyrenees": [], "redbone": [], "retriever": ["chesapeake", "curly", "flatcoated", "golden"], "ridgeback": ["rhodesian"], "rottweiler": [], "saluki": [], "samoyed": [], "schipperke": [], "schnauzer": ["giant", "miniature"], "setter": ["english", "gordon", "irish"], "sheepdog": ["english", "shetland"], "shiba": [], "shihtzu": [], "spaniel": ["blenheim", "brittany", "cocker", "irish", "japanese", "sussex", "welsh"], "springer": ["english"], "stbernard": [], "terrier": [ "american", "australian", "bedlington", "border", "dandie", "fox", "irish", "kerryblue", "lakeland", "norfolk", "norwich", "patterdale", "scottish", "sealyham", "silky", "tibetan", "toy", "westhighland", "wheaten", "yorkshire" ], "vizsla": [], "weimaraner": [], "whippet": [], "wolfhound": ["irish"] }</code></p>
<h4 id="after">After</h4>
<p><code>[ { "dogBreedItemId": 1, "breedName": "affenpinscherrrrr", "subBreeds": [] }, { "dogBreedItemId": 2, "breedName": "african", "subBreeds": [] }, { "dogBreedItemId": 3, "breedName": "airedale", "subBreeds": [] }, { "dogBreedItemId": 4, "breedName": "akita", "subBreeds": [] }, { "dogBreedItemId": 5, "breedName": "appenzeller", "subBreeds": [] }, { "dogBreedItemId": 6, "breedName": "basenji", "subBreeds": [] }, { "dogBreedItemId": 7, "breedName": "beagle", "subBreeds": [] }, { "dogBreedItemId": 8, "breedName": "bluetick", "subBreeds": [] }, { "dogBreedItemId": 9, "breedName": "borzoi", "subBreeds": [] }, { "dogBreedItemId": 10, "breedName": "bouvier", "subBreeds": [] }, { "dogBreedItemId": 11, "breedName": "boxer", "subBreeds": [] }, { "dogBreedItemId": 12, "breedName": "brabancon", "subBreeds": [] }, { "dogBreedItemId": 13, "breedName": "briard", "subBreeds": [] }, { "dogBreedItemId": 14, "breedName": "bulldog", "subBreeds": [ { "parentBreedId": 14, "dogSubBreedId": 1, "subBreedName": "boston" }, { "parentBreedId": 14, "dogSubBreedId": 2, "subBreedName": "french" } ] }, { "dogBreedItemId": 15, "breedName": "bullterrier", "subBreeds": [ { "parentBreedId": 15, "dogSubBreedId": 3, "subBreedName": "staffordshire" } ] }, { "dogBreedItemId": 16, "breedName": "cairn", "subBreeds": [] }, { "dogBreedItemId": 17, "breedName": "chihuahua", "subBreeds": [] }, { "dogBreedItemId": 18, "breedName": "chow", "subBreeds": [] }, { "dogBreedItemId": 19, "breedName": "clumber", "subBreeds": [] }, { "dogBreedItemId": 20, "breedName": "collie", "subBreeds": [ { "parentBreedId": 20, "dogSubBreedId": 4, "subBreedName": "border" } ] }, { "dogBreedItemId": 21, "breedName": "coonhound", "subBreeds": [] }, { "dogBreedItemId": 22, "breedName": "corgi", "subBreeds": [ { "parentBreedId": 22, "dogSubBreedId": 5, "subBreedName": "cardigan" } ] }, { "dogBreedItemId": 23, "breedName": "dachshund", "subBreeds": [] }, { "dogBreedItemId": 24, "breedName": "dane", "subBreeds": [ { "parentBreedId": 24, "dogSubBreedId": 6, "subBreedName": "great" } ] }, { "dogBreedItemId": 25, "breedName": "deerhound", "subBreeds": [ { "parentBreedId": 25, "dogSubBreedId": 7, "subBreedName": "scottish" } ] }, { "dogBreedItemId": 26, "breedName": "dhole", "subBreeds": [] }, { "dogBreedItemId": 27, "breedName": "dingo", "subBreeds": [] }, { "dogBreedItemId": 28, "breedName": "doberman", "subBreeds": [] }, { "dogBreedItemId": 29, "breedName": "elkhound", "subBreeds": [ { "parentBreedId": 29, "dogSubBreedId": 8, "subBreedName": "norwegian" } ] }, { "dogBreedItemId": 30, "breedName": "entlebucher", "subBreeds": [] }, { "dogBreedItemId": 31, "breedName": "eskimo", "subBreeds": [] }, { "dogBreedItemId": 32, "breedName": "germanshepherd", "subBreeds": [] }, { "dogBreedItemId": 33, "breedName": "greyhound", "subBreeds": [ { "parentBreedId": 33, "dogSubBreedId": 9, "subBreedName": "italian" } ] }, { "dogBreedItemId": 34, "breedName": "groenendael", "subBreeds": [] }, { "dogBreedItemId": 35, "breedName": "hound", "subBreeds": [ { "parentBreedId": 35, "dogSubBreedId": 10, "subBreedName": "Ibizan" }, { "parentBreedId": 35, "dogSubBreedId": 11, "subBreedName": "afghan" }, { "parentBreedId": 35, "dogSubBreedId": 12, "subBreedName": "basset" }, { "parentBreedId": 35, "dogSubBreedId": 13, "subBreedName": "blood" }, { "parentBreedId": 35, "dogSubBreedId": 14, "subBreedName": "english" }, { "parentBreedId": 35, "dogSubBreedId": 15, "subBreedName": "walker" } ] }, { "dogBreedItemId": 36, "breedName": "husky", "subBreeds": [] }, { "dogBreedItemId": 37, "breedName": "keeshond", "subBreeds": [] }, { "dogBreedItemId": 38, "breedName": "kelpie", "subBreeds": [] }, { "dogBreedItemId": 39, "breedName": "komondor", "subBreeds": [] }, { "dogBreedItemId": 40, "breedName": "kuvasz", "subBreeds": [] }, { "dogBreedItemId": 41, "breedName": "labrador", "subBreeds": [] }, { "dogBreedItemId": 42, "breedName": "leonberg", "subBreeds": [] }, { "dogBreedItemId": 43, "breedName": "lhasa", "subBreeds": [] }, { "dogBreedItemId": 44, "breedName": "malamute", "subBreeds": [] }, { "dogBreedItemId": 45, "breedName": "malinois", "subBreeds": [] }, { "dogBreedItemId": 46, "breedName": "maltese", "subBreeds": [] }, { "dogBreedItemId": 47, "breedName": "mastiff", "subBreeds": [ { "parentBreedId": 47, "dogSubBreedId": 16, "subBreedName": "bull" }, { "parentBreedId": 47, "dogSubBreedId": 17, "subBreedName": "tibetan" } ] }, { "dogBreedItemId": 48, "breedName": "mexicanhairless", "subBreeds": [] }, { "dogBreedItemId": 49, "breedName": "mountain", "subBreeds": [ { "parentBreedId": 49, "dogSubBreedId": 18, "subBreedName": "bernese" }, { "parentBreedId": 49, "dogSubBreedId": 19, "subBreedName": "swiss" } ] }, { "dogBreedItemId": 50, "breedName": "newfoundland", "subBreeds": [] }, { "dogBreedItemId": 51, "breedName": "otterhound", "subBreeds": [] }, { "dogBreedItemId": 52, "breedName": "papillon", "subBreeds": [] }, { "dogBreedItemId": 53, "breedName": "pekinese", "subBreeds": [] }, { "dogBreedItemId": 54, "breedName": "pembroke", "subBreeds": [] }, { "dogBreedItemId": 55, "breedName": "pinscher", "subBreeds": [ { "parentBreedId": 55, "dogSubBreedId": 20, "subBreedName": "miniature" } ] }, { "dogBreedItemId": 56, "breedName": "pointer", "subBreeds": [ { "parentBreedId": 56, "dogSubBreedId": 21, "subBreedName": "german" } ] }, { "dogBreedItemId": 57, "breedName": "pomeranian", "subBreeds": [] }, { "dogBreedItemId": 58, "breedName": "poodle", "subBreeds": [ { "parentBreedId": 58, "dogSubBreedId": 22, "subBreedName": "miniature" }, { "parentBreedId": 58, "dogSubBreedId": 23, "subBreedName": "standard" }, { "parentBreedId": 58, "dogSubBreedId": 24, "subBreedName": "toy" } ] }, { "dogBreedItemId": 59, "breedName": "pug", "subBreeds": [] }, { "dogBreedItemId": 60, "breedName": "pyrenees", "subBreeds": [] }, { "dogBreedItemId": 61, "breedName": "redbone", "subBreeds": [] }, { "dogBreedItemId": 62, "breedName": "retriever", "subBreeds": [ { "parentBreedId": 62, "dogSubBreedId": 25, "subBreedName": "chesapeake" }, { "parentBreedId": 62, "dogSubBreedId": 26, "subBreedName": "curly" }, { "parentBreedId": 62, "dogSubBreedId": 27, "subBreedName": "flatcoated" }, { "parentBreedId": 62, "dogSubBreedId": 28, "subBreedName": "golden" } ] }, { "dogBreedItemId": 63, "breedName": "ridgeback", "subBreeds": [ { "parentBreedId": 63, "dogSubBreedId": 29, "subBreedName": "rhodesian" } ] }, { "dogBreedItemId": 64, "breedName": "rottweiler", "subBreeds": [] }, { "dogBreedItemId": 65, "breedName": "saluki", "subBreeds": [] }, { "dogBreedItemId": 66, "breedName": "samoyed", "subBreeds": [] }, { "dogBreedItemId": 67, "breedName": "schipperke", "subBreeds": [] }, { "dogBreedItemId": 68, "breedName": "schnauzer", "subBreeds": [ { "parentBreedId": 68, "dogSubBreedId": 30, "subBreedName": "giant" }, { "parentBreedId": 68, "dogSubBreedId": 31, "subBreedName": "miniature" } ] }, { "dogBreedItemId": 69, "breedName": "setter", "subBreeds": [ { "parentBreedId": 69, "dogSubBreedId": 32, "subBreedName": "english" }, { "parentBreedId": 69, "dogSubBreedId": 33, "subBreedName": "gordon" }, { "parentBreedId": 69, "dogSubBreedId": 34, "subBreedName": "irish" } ] }, { "dogBreedItemId": 70, "breedName": "sheepdog", "subBreeds": [ { "parentBreedId": 70, "dogSubBreedId": 35, "subBreedName": "english" }, { "parentBreedId": 70, "dogSubBreedId": 36, "subBreedName": "shetland" } ] }, { "dogBreedItemId": 71, "breedName": "shiba", "subBreeds": [] }, { "dogBreedItemId": 72, "breedName": "shihtzu", "subBreeds": [] }, { "dogBreedItemId": 73, "breedName": "spaniel", "subBreeds": [ { "parentBreedId": 73, "dogSubBreedId": 37, "subBreedName": "blenheim" }, { "parentBreedId": 73, "dogSubBreedId": 38, "subBreedName": "brittany" }, { "parentBreedId": 73, "dogSubBreedId": 39, "subBreedName": "cocker" }, { "parentBreedId": 73, "dogSubBreedId": 40, "subBreedName": "irish" }, { "parentBreedId": 73, "dogSubBreedId": 41, "subBreedName": "japanese" }, { "parentBreedId": 73, "dogSubBreedId": 42, "subBreedName": "sussex" }, { "parentBreedId": 73, "dogSubBreedId": 43, "subBreedName": "welsh" } ] }, { "dogBreedItemId": 74, "breedName": "springer", "subBreeds": [ { "parentBreedId": 74, "dogSubBreedId": 44, "subBreedName": "english" } ] }, { "dogBreedItemId": 75, "breedName": "stbernard", "subBreeds": [] }, { "dogBreedItemId": 76, "breedName": "terrier", "subBreeds": [ { "parentBreedId": 76, "dogSubBreedId": 45, "subBreedName": "american" }, { "parentBreedId": 76, "dogSubBreedId": 46, "subBreedName": "australian" }, { "parentBreedId": 76, "dogSubBreedId": 47, "subBreedName": "bedlington" }, { "parentBreedId": 76, "dogSubBreedId": 48, "subBreedName": "border" }, { "parentBreedId": 76, "dogSubBreedId": 49, "subBreedName": "dandie" }, { "parentBreedId": 76, "dogSubBreedId": 50, "subBreedName": "fox" }, { "parentBreedId": 76, "dogSubBreedId": 51, "subBreedName": "irish" }, { "parentBreedId": 76, "dogSubBreedId": 52, "subBreedName": "kerryblue" }, { "parentBreedId": 76, "dogSubBreedId": 53, "subBreedName": "lakeland" }, { "parentBreedId": 76, "dogSubBreedId": 54, "subBreedName": "norfolk" }, { "parentBreedId": 76, "dogSubBreedId": 55, "subBreedName": "norwich" }, { "parentBreedId": 76, "dogSubBreedId": 56, "subBreedName": "patterdale" }, { "parentBreedId": 76, "dogSubBreedId": 57, "subBreedName": "scottish" }, { "parentBreedId": 76, "dogSubBreedId": 58, "subBreedName": "sealyham" }, { "parentBreedId": 76, "dogSubBreedId": 59, "subBreedName": "silky" }, { "parentBreedId": 76, "dogSubBreedId": 60, "subBreedName": "tibetan" }, { "parentBreedId": 76, "dogSubBreedId": 61, "subBreedName": "toy" }, { "parentBreedId": 76, "dogSubBreedId": 62, "subBreedName": "westhighland" }, { "parentBreedId": 76, "dogSubBreedId": 63, "subBreedName": "wheaten" }, { "parentBreedId": 76, "dogSubBreedId": 64, "subBreedName": "yorkshire" } ] }, { "dogBreedItemId": 77, "breedName": "vizsla", "subBreeds": [] }, { "dogBreedItemId": 78, "breedName": "weimaraner", "subBreeds": [] }, { "dogBreedItemId": 79, "breedName": "whippet", "subBreeds": [] } ]</code></p>

