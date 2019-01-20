import React, { Component } from 'react';
import ReactJson from 'react-json-view'

export class Counter extends Component {
    displayName = Counter.name

    constructor(props) {
        super(props);
        this.state = { currentCount: 0, jsonPayload: this.populateJsonFromUrl()}
        this.incrementCounter = this.incrementCounter.bind(this);
    }

    incrementCounter() {
        this.setState({
            currentCount: this.state.currentCount + 1
        });
    }

    render() {
        return (
            <div>
                <h1>Json Viewer</h1>

                <p>Veiw the raw json in a nice format.</p>

                <ReactJson src={this.state.jsonPayload} />

                <p>Current count: <strong>{this.state.currentCount}</strong></p>

                <button onClick={this.incrementCounter}>Increment</button>
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
