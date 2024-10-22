import React, {Component} from 'react';
import './CV.css';
import Employment from "./Employment";
import Person from "./Person";

export class CV extends Component {

    personId;

    constructor(props) {
        super(props);

        this.personId = 1; // Oh-oh, naughty magic numbers to get Andrei from backend :)

        this.state = {person: null, employments: null, technologies: null, hobbies: null, education: null};
    }

    componentDidMount() {
        fetch("/api/v2/people/" + this.personId)
            .then(async s => {

                const data = await s.json();

                this.setState({person: data});
            });

        fetch("/api/v2/people/" + this.personId + "/employments")
            .then(async resp => {
                const data = await resp.json();

                this.setState({employments: data.items});
            });
        

        fetch("/api/v2/people/" + this.personId + "/technologies")
            .then(async resp => {
                const data = await resp.json();

                this.setState({technologies: data.items})
            });

        fetch("/api/v2/people/" + this.personId + "/education")
            .then(async resp => {
                const data = await resp.json();
                this.setState({education: data.items});
            })

        fetch("/api/v2/people/" + this.personId + "/hobbies")
            .then(async resp => {
                const data = await resp.json();
                this.setState({hobbies: data.items});
            })
    }

    loading() {
        return <span>Loading..</span>
    }

    render() {
        return <div>
            <header className="section-header">
                {this.state.person ? <Person person={this.state.person}/> : this.loading()}
            </header>

            <section className="section">
                <h2>Professional Summary</h2>
                {this.state.person ? <p>{this.state.person.summary}</p> : this.loading()}
            </section>

            <section className="section">
                <h2>Experience</h2>
                <div>
                    {this.state.employments ? this.state.employments.map(e =>
                        <Employment employment={e}/>
                    ) : this.loading()}
                </div>
            </section>

            <section className="section">
                <h2 id="technologies">Skills / Technologies</h2>
                <ul>
                    {this.state.technologies ? this.state.technologies.map(s =>
                        <li key={s.title}>
                            {s.title}
                            <span className="assignment-summary">
                                ({s.level})
                            </span>
                        </li>
                    ) : this.loading()}
                </ul>
            </section>

            <section className="section">
                <h2>Education</h2>
                {this.state.education ? this.state.education.map(education =>

                    <p>{education.name} - {education.inititution} ({education.yearStarted} - {education.yearFinished})</p>
                ) : this.loading()}


            </section>

            <section className="section">
                <h2>Personal</h2>
                <ul>
                    {this.state.hobbies && this.state.hobbies.map(hobbie =>

                        <li key={hobbie}>{hobbie}</li>
                    )}
                </ul>
            </section>
        </div>
    }
}