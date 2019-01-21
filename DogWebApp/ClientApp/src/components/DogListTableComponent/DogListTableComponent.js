import React, { Component } from 'react';
import ReactDOM from "react-dom";
import './DogListTableComponent.css';

const Modal = ({ handleClose, show, children }) => {
    const showHideClassName = show ? "modal display-block" : "modal display-none";

    return (
        <div className={showHideClassName}>
            <section className="modal-main">
                {children}
                <button onClick={handleClose}>close</button>
            </section>
        </div>
    );
};

export class DogListTableComponent extends Component {
    displayName = DogListTableComponent.name

    constructor(props) {
        super(props);
        this.state = {
            postResponse: "ResponseMessage",
            dogBreeds: [],
            loading: true,
            showModalPopup: false
        };
        this.showModal = this.showModal.bind(this);
        this.hideModal = this.hideModal.bind(this);
        this.deleteBreed = this.deleteBreed.bind(this);
    }

    static renderForecastsTable(dogBreeds) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>

                        </th>
                        <th>Dog Breed Id</th>
                        <th>Dog Breed</th>
                        <th>Sub Breeds</th>
                    </tr>
                </thead>
                <tbody>
                    {dogBreeds.map(dogBreed =>
                        <tr key={dogBreed.dogBreedItemId}>
                            <td><button onClick={breed => this.deleteBreed(dogBreed)}>-</button>
                                <button>^</button>
                            </td>
                            <td>{dogBreed.dogBreedItemId}</td>
                            <td>{dogBreed.breedName}</td>
                            <td>
                                {dogBreed.subBreeds.map(subBreed =>
                                    <table className='subBreedTable'>
                                        <tbody>
                                            <tr key={subBreed.parentBreedId}>
                                                <td><button className="addSubBreed">+</button></td>
                                                <td><button className="removeSubBreed">-</button></td>
                                                <td>{subBreed.subBreedName}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                )}
                            </td>
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
        this.setState({ loading: false });
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : DogListTableComponent.renderForecastsTable(this.state.dogBreeds);

        return (
            <div>
                <h1>Dog Breed Table</h1>
                <p>Easy, Human Readable List</p>

                <Modal show={this.state.showModalPopup} handleClose={this.hideModal}>
                    <h1>Add Breed</h1>
                    <form onSubmit={this.postDogBreed}>
                        <label>
                            Breed Name: <input type="text" name="breedName" />
                        </label>
                        <input type="submit" value="Submit" />
                        <label>
                            {this.state.postResponse}
                        </label>
                    </form>
                </Modal>

                <button onClick={this.showModal}>Add Breed </button>
                {contents}
            </div>
        );
    }

    postDogBreed(event) {
        event.preventDefault();
        const data = new (FormData)(event.target);

        var object = {};
        data.forEach(function (value, key) {
            object[key] = value;
        });
        var json = JSON.stringify(object);

        console.log(json);

        fetch('https://raedogrestapi.azurewebsites.net/api/dogbreed/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: json,
        }).then(res => res.json())
            .then(response => console.log('Success:', JSON.stringify(response)))
            .then(response => this.setState({ postResponse : response }))
            .catch(error => console.error('Error:', error));
    }

    deleteBreed = breed => {
        var url = 'https://raedogrestapi.azurewebsites.net/api/dogbreed/' + breed;  
        fetch(url, {
            method: 'DELETE',
        }).then(res => res.json())
            .then(response => console.log('Success:', JSON.stringify(response)))
            .then(response => this.setState({ postResponse: response }))
            .catch(error => console.error('Error:', error));
    }

    showModal() {
        this.setState({ showModalPopup: true });
    }

    hideModal = () => {
        this.setState({ showModalPopup: false });
    }
}

