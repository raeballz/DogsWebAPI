import React, { Component } from 'react';

//github.com/mac-s-g/react-json-view
import ReactJson from 'react-json-view'

export class Counter extends Component {
    displayName = Counter.name

    constructor(props) {
        super(props);
        this.state = { currentCount: 0, jsonPayload: this.populateJsonFromUrl()}
    }

    regenerateDatabase(item, url) {
        return fetch('https://raedogrestapi.azurewebsites.net/api/dogbreed/-1', {
            method: 'delete'
        }).then(response =>
            response.json().then(json => {
                return json;
            })
        )
    }

    render() {
        return (
            <div>
                <h1>Json Viewer</h1>

                <p>View the raw json in a nice format.</p>

                <p>Current count: <strong>{this.state.currentCount}</strong></p>

                <button onClick={this.regenerateDatabase}>Regenerate Database</button>

                <ReactJson src={this.state.jsonPayload} />
            </div>
        );
    }

    populateJsonFromUrl = async () => {
        var url = 'https://raedogrestapi.azurewebsites.net/api/dogbreed';

        fetch(url)
            .then((response) => response.json())
            .then(x => this.setState({ jsonPayload: x }))
    }
}
