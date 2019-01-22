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
            breedToAddSubBreedTo: 0,
        };
        this.showModal = this.showModal.bind(this);
        this.hideModal = this.hideModal.bind(this);
        this.showSubBreedModal = this.showSubBreedModal.bind(this);
        this.hideSubBreedModal = this.hideSubBreedModal.bind(this);
        this.fetchJsonList = this.fetchJsonList.bind(this);
        this.postDogBreed = this.postDogBreed.bind(this);
        this.addSubBreed = this.addSubBreed.bind(this);
        this.deleteBreed = this.deleteBreed.bind(this);
        this.deleteSubBreed = this.deleteSubBreed.bind(this);
    }

    renderForecastsTable(dogBreeds) {
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
                            <td><button className="DeleteBreedBtn" id={dogBreed.dogBreedItemId} onClick={this.deleteBreed}>-</button>
                            </td>
                            <td>{dogBreed.dogBreedItemId}</td>
                            <td>{dogBreed.breedName}</td>
                            <td>
                                <button className="SubBreedAddBtn" id={dogBreed.dogBreedItemId} onClick={this.showSubBreedModal} handleClose={this.hideSubBreedModal}>+ Sub Breed</button>
                                {dogBreed.subBreeds.map(subBreed =>
                                    <table className='subBreedTable'>
                                        <tbody>
                                            <tr key={subBreed.parentBreedId}>
                                                <td><button className={subBreed.parentBreedId} id={subBreed.dogSubBreedId} onClick={this.deleteSubBreed}>-</button></td>
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
        this.setState({ loading: true });
        this.fetchJsonList();
    }

    render() {
        let contents = this.state.loading ? <p>"Loading..."</p> : this.renderForecastsTable(this.state.dogBreeds);
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

                <Modal show={this.state.showSubBreedModal} handleClose={this.hideSubBreedModal}>
                    <h1>Add SubBreed</h1>
                    <form onSubmit={this.addSubBreed}>
                        <label>
                            Sub Breed Name: <input type="text" name="subBreedName" />
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
            .then(response => alert('Successfully Added: ' + JSON.stringify(response)))
            .catch(error => console.error('Error:', error));
    }

    addSubBreed(event) {
        var parentId = this.state.breedToAddSubBreedTo;
        const data = new (FormData)(event.target);

        var object = {};
        data.forEach(function (value, key) {
            object[key] = value;
        });
        var json = JSON.stringify(object);
        
        var url = 'https://raedogrestapi.azurewebsites.net/api/dogbreed/' + parentId + '/subbreed';
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: json,
        }).then(res => res.json())
            .then(response => this.setState({ addSubBreedRespose: JSON.stringify(response) }))
            .then(() => alert('Successfully Added: ' + this.state.addSubBreedRespose))
            .then(() => console.log('Success:', this.state.addSubBreedRespose))
            .catch(error => console.error('Error:', error));
        this.setState(this.state);
    }

    deleteBreed(breed) {
        var id = breed.target.id;
        var url = 'https://raedogrestapi.azurewebsites.net/api/dogbreed/' + id;  
        fetch(url, {
            method: 'DELETE',
        }).then(res => res.json())
            .then(response => this.setState({ deleteResponse: JSON.stringify(response) }))
            .then(() => alert('Successfully removed: ' + this.state.deleteResponse))
            .then(() => this.fetchJsonList())
            .then(() => console.log('Success:', this.state.deleteResponse))
            .catch(error => console.error('Error:', error));
        this.setState(this.state);
    }

    deleteSubBreed(subBreedRemoveButton) {
        var subBreedId = subBreedRemoveButton.target.id;
        var parentBreedId = subBreedRemoveButton.target.className;
        var URL = 'https://raedogrestapi.azurewebsites.net/api/dogbreed/' + parentBreedId + "/subbreed/" + subBreedId;

        fetch(URL, {
            method: 'DELETE'
        }).then(res => res.json())
            .then(response => this.setState({ deleteSubBreedResponse: JSON.stringify(response) }))
            .then(() => alert('Successfully removed: ' + this.state.deleteSubBreedResponse))
            .then(() => this.fetchJsonList())
            .then(console.log('Success:', this.state.deleteSubBreedResponse))
            .catch(error => console.error('Error:', error));
        this.setState(this.state);
    }

    showModal() {
        this.setState({ showModalPopup: true });
    }

    hideModal = () => {
        this.setState({ showModalPopup: false });
    }

    showSubBreedModal(e) {        
        this.setState({ showSubBreedModal: true, breedToAddSubBreedTo: e.target.id });
    }

    hideSubBreedModal = () => {
        this.setState({ showSubBreedModal: false, breedToAddSubBreedTo: "" });
    }

    fetchJsonList() {
        this.setState({ loading: true });
        var url = 'https://raedogrestapi.azurewebsites.net/api/dogbreed';
        fetch(url)
            .then((response) => response.json())
            .then(x => this.setState({ dogBreeds: x, loading:false }))            
            .then(x => this.forceUpdate());        
    }
}

