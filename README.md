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
<li>Separated from REST client, can switch out URLs to point to different data sources.</li>
<li>Gives UI to see the DB structure, nicely formatted as a JSON payload.</li>
<li>Gives UI to see all dog breeds and sub-breeds in a table form. UI buttons to Add and Delete breeds and sub-breeds. Gives modal popups with a form for easy user input.</li>
<li>Page stays in sync with server on data update via React states.</li>
<li>Links to additional information.</li>
<li>Deployed via click-once to Azure Cloud.</li>
</ul>

