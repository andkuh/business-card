import React, {Component} from 'react';
import './CV.css';
import Employment from "./Employment";
import Person from "./Person";

export class CV extends Component {

    constructor(props) {
        super(props);

        this.state = {person: {}, employments: [], technologies: []};
    }
    
    componentDidMount() {
        fetch("/api/v2/people/1")
            .then(async s => {

                const data = await s.json();

                this.setState({person: data});
            });

        fetch("/api/people/1/employments")
            .then(async resp => {
                const data = await resp.json();

                this.setState({employments: data.items});
            });

        fetch("/api/technologies")
            .then(async resp => {
                const data = await resp.json();

                this.setState({technologies: data.items})
            })
    }

    render() {
        return <div>
            <header className="section-header">
                {this.state && <Person person={this.state.person}/>} 
            </header>

            <section className="section">
                <h2>Professional Summary</h2>
                <p>{this.state.person.summary}</p>
            </section>

            <section className="section">
                <h2>Experience</h2>
                <div>
                    {this.state.employments.map(e =>
                        <Employment employment={e}/>
                    )}
                </div>
            </section>
            
            <section className="section">
                <h2 id="technologies">Skills / Technologies</h2>
                <ul>
                    {this.state.technologies.map(s =>
                        <li key={s.title}>
                            {s.title}
                        </li>
                    )}
                </ul>
            </section>

            <section className="section">
                <h2>Education</h2>
                <p>Bachelor's Degree in Finance - BSTU (2010)</p>
            </section>

            <section className="section">
                <h2>Personal</h2>
                <ul>
                    <li>Musician</li>
                    <li>Bike Traveller</li>
                    <li>Cat Person</li>
                </ul>
            </section>
        </div>
    }
}