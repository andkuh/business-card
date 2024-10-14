import React, {Component} from 'react';
import {CV} from "./CV";

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <CV/>
            </div>
        );
    }
}
