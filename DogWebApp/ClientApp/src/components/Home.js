import React, { Component } from 'react';

export class Home extends Component {
    displayName = Home.name

    render() {
        return (
            <div>
                <h1>Welcome To The Dog Parser</h1>
                <p>Here for parsing your canine parsing breeds and needs!</p>
                <ul>
                    <li><a href='https://github.com/raeballz/DogsWebAPI/'>Repository Link</a> hosted @ Github</li>
                    <li><a href='https://github.com/raeballz/DogsWebAPI/projects/1'>Project Backlog Link</a> hosted @ Github</li>
                    <li><a href='https://github.com/raeballz/DogsWebAPI/network'>Git Commit Log</a> hosted @ Github </li>
                    <li><a href='https://raedogrestapi.azurewebsites.net/api/dogbreed'>Raw API </a> hosted @ Azure </li>

                </ul>
            </div>
        );
    }
}
