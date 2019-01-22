import React, { Component } from 'react';
import ReactDOM from "react-dom";
import './DogListTableComponent.css';
import { Glyphicon, Button, Table} from 'react-bootstrap';


const Modal = ({ handleClose, show, children }) => {
    const showHideClassName = show ? "modal display-block" : "modal display-none";

    return (
        <div className={showHideClassName}>
            <section className="modal-main">
                <Button bsSize="small"
                        className="closeModal"
                        onClick={handleClose} >
                    <Glyphicon glyph='glyphicon glyphicon-remove'/> Close
                </Button>    
                {children}
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
            <Table striped bordered hover responsive className='table'>
                <thead>
                    <tr>
                        <th>
                            <Button block bsStyle="info" onClick={this.showModal}>+ Add Breed </Button>
                        </th>
                        <th>Dog Breed Id</th>
                        <th>Dog Breed</th>
                        <th>Sub Breeds</th>
                    </tr>
                </thead>
                <tbody>
                    {dogBreeds.map(dogBreed =>
                        <tr key={dogBreed.dogBreedItemId}>
                            <td onClick={this.deleteBreed} id={dogBreed.dogBreedItemId}>
                                <Button deleteBreedButton bsStyle="error" block bsSize="xsmall">
                                    <Glyphicon glyph="glyphicon glyphicon-remove" size="small" />
                                </Button>
                            </td>
                            <td>{dogBreed.dogBreedItemId}</td>
                            <td>{dogBreed.breedName}</td>
                            <td>
                                <Button className="SubBreedAddBtn"
                                        id={dogBreed.dogBreedItemId}
                                        onClick={this.showSubBreedModal}
                                        handleClose={this.hideSubBreedModal}
                                        bsStyle="info"
                                        block
                                        >+ Add Sub Breed</Button>
                                {dogBreed.subBreeds.map(subBreed =>
                                    <Table responsive className='SubBreedTable'>
                                        <thead>
                                            <th></th>
                                            <th></th>
                                        </thead>
                                        <tbody>
                                            <tr key={subBreed.parentBreedId}>
                                                <td className={subBreed.parentBreedId}
                                                    id={subBreed.dogSubBreedId}
                                                    onClick={this.deleteSubBreed}
                                                    width="20">
                                                    <Button bsStyle="error" block bsSize="xsmall">
                                                        <Glyphicon glyph="glyphicon glyphicon-remove" size="small" />
                                                    </Button>
                                                </td>
                                                <td>{subBreed.subBreedName}</td>
                                            </tr>
                                        </tbody>
                                    </Table>
                                )}
                            </td>
                        </tr>
                    )}
                </tbody>
            </Table>
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

                <Modal show={this.state.showModalPopup}
                       handleClose={this.hideModal}>
                    <h1>Add Breed</h1>
                    <form onSubmit={this.postDogBreed}>
                        <label>
                            Breed Name: <input type="text" name="breedName" />

                            <input type="submit" value="Submit" />
                        </label>
                    </form>
                </Modal>

                <Modal show={this.state.showSubBreedModal}
                       handleClose={this.hideSubBreedModal}>
                    <h1>Add SubBreed</h1>
                    <form onSubmit={this.addSubBreed}>
                        <label>
                            Sub Breed Name: <input type="text" name="subBreedName" />
                        
                            <input type="submit" value="Submit" />
                        </label>
                    </form>
                </Modal>
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
            .then(response => this.setState({ addBreedResponse: JSON.stringify(response) }))
            .then(() => alert('Successfully Added: ' + this.state.addBreedResponse))
            .then(() => this.fetchJsonList())
            .then(() => console.log('Success:', this.state.addBreedResponse))
            .catch(error => console.error('Error:', error));
        this.setState(this.state);
    }

    addSubBreed(event) {
        event.preventDefault();
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
            .then(response => this.setState({ addSubBreedResponse: JSON.stringify(response) }))
            .then(() => alert('Successfully Added: ' + this.state.addSubBreedResponse))
            .then(() => this.fetchJsonList())
            .then(() => console.log('Success:', this.state.addSubBreedResponse))
            .catch(error => console.error('Error:', error));
        this.setState(this.state);
    }

    deleteBreed(breed) {
        var id = breed.currentTarget.id;
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
        var subBreedId = subBreedRemoveButton.currentTarget.id;
        var parentBreedId = subBreedRemoveButton.currentTarget.className;
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

