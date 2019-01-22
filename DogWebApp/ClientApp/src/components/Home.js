import React, { Component } from 'react';
import { Table, PageHeader } from 'react-bootstrap';

export class Home extends Component {
    displayName = Home.name

    render() {
        return (
            <div>
                <PageHeader>
                    Welcome To The Dog Parser <br />
                    <small> Here for parsing your canine parsing breeds and needs! </small>
                </PageHeader>
                <p>This responsive React web app will allow you to interact with the REST api in a visual manner</p>
                <p>This page should happily render on desktop, mobile and tablet.</p>

                <p>Below, some links are provided to give you more information on the project.</p>

                <p>The JSON viewer will allow you to look at the current DB state in a json payload format. </p>
                <p>The Breed table will allow you to interact with the list directly, updating the DB via REST calls.</p>
                <p>This page is hosted on a different cloud instance from the REST API, so if the API goes down, the web page won't.</p>

                <Table striped>
                    <thead>
                        <th> Resource </th>
                        <th> Link </th>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Code Repository + Documentation:</td>
                            <td>
                                <a href='https://github.com/raeballz/DogsWebAPI/'>Repository Link</a>
                            </td>
                        </tr>
                        <tr>
                            <td>Project Backlog:</td>
                            <td>
                                <a href='https://github.com/raeballz/DogsWebAPI/projects/1'>Repository Link</a>
                            </td>
                        </tr>
                        <tr>
                            <td>Git Commit Tree:</td>
                            <td>
                                <a href='https://github.com/raeballz/DogsWebAPI/network'>Repository Link</a>
                            </td>
                        </tr>
                        <tr>
                            <td>Raw Rest API:</td>
                            <td>
                                <a href='https://raedogrestapi.azurewebsites.net/api/dogbreed'>Repository Link</a>
                            </td>
                        </tr>
                    </tbody>
                </Table>
            </div>
        );
    }
}
