import React, { Component } from 'react';

export class FetchData extends Component {
    displayName = FetchData.name

    constructor(props) {
        super(props);
        this.state = {
            dogBreeds: []
        };
        this.getJson();
    }

    static renderForecastsTable(dogBreeds) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>DogBreed</th>
                        <th>Dog Breed Id</th>
                        <th>Breed Id</th>
                        <th>Sub Breeds</th>
                    </tr>
                </thead>
                <tbody>
                    {dogBreeds.map(dogBreed =>
                        <tr key={dogBreed.dogBreedItemId}>
                            <td>{dogBreed.breedName}</td>
                            <td>{dogBreed.subBreeds}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderForecastsTable(this.state.dogBreeds);

        return (
            <div>
                <h1>Dog Breed Table</h1>
                <p>Easy, Human Readable List</p>
                {contents}
            </div>
        );
    }

    getJson() {
        fetch('https://raedogrestapi.azurewebsites.net/api/dogbreed')
            .then(function (response) {
                this.state.setState().dogBreeds = response.json();
            })
            .then(function (myJson) {
                console.log(JSON.stringify(myJson));
            });
    }
}
