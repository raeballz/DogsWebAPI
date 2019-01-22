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
<p>REST API</p>

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
<td><code>Example delete</code></td>
<td>STATUS: 200, STATUS: 404</td>
</tr>
<tr>
<td>DELETE</td>
<td>/api/dogbreed/{id}/subbreed/{subBreedId}</td>
<td>Deletes a sub-breed by Id within a breed by ID</td>
<td><code>Example delete</code></td>
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
<td>{“dogBreedItemId”:1,“breedName”:“affenpinscherrrrr”,“subBreeds”: []}</td>
<td></td>
</tr>
</tbody>
</table>
