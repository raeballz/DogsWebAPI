import React, { Component } from 'react';

export class FetchData extends Component {
    displayName = FetchData.name

    constructor(props) {
        super(props);
        this.state = {
            dogBreeds: [],
            loading : true,
        };
    }

    static renderForecastsTable(dogBreeds) {
        this.getJson;
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

                            
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    componentDidMount() {

        var url = 'https://raedogrestapi.azurewebsites.net/api/dogbreed';
        fetch(url)
            .then((response) => response.json())
            .then(x => this.setState({ dogBreeds: x }))
        this.setState({loading: false})
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
}
