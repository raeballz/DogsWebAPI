import React, { Component } from 'react';
import logo from './logo.svg';
import jsonfile from './dogs.json'
import './App.css';
import ReactJson from 'react-json-view'
import { isRejected } from 'q';

class App extends Component {

  constructor()
  {
    super();
    this.state = {
      jsonPayload : this.populateJsonFromUrl()
    }    
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
           Dog Parser
          </p>          
        </header>
        <ReactJson src={this.state.jsonPayload} theme="monokai" />
        <div>Icons made by <a href="https://www.freepik.com/" title="Freepik">Freepik</a> from <a href="https://www.flaticon.com/" 			    title="Flaticon">www.flaticon.com</a> is licensed by <a href="http://creativecommons.org/licenses/by/3.0/" 			    title="Creative Commons BY 3.0" target="_blank">CC 3.0 BY</a></div>
      </div>
    );
  }

  populateJsonFromUrl = async() => {
    var url = 'https://raedogrestapi.azurewebsites.net/api/dogbreed';

    fetch(url)
    .then((response) => response.json())
    .then(x => this.setState( {jsonPayload: x}))
    
  }    
}

export default App;
