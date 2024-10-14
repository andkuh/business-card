import React, {Component} from 'react';
import './CV.css';
import {formatDateToMonthAndYear} from "../FormatDate";

export class CV extends Component {

    constructor(props) {
        super(props);

        this.state = {person: {}, employments: [], technologies: []};
    }


    componentDidMount() {
        fetch("/api/v2/people/1")
            .then(async s => {

                const data = await s.json();

                this.setState({person: data})
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
                <div>
                    {this.state.person.id &&
                    <div>
                        <h1>{this.state.person.firstName} {this.state.person.lastName}</h1>
                        <p>{this.state.person.specialization}, {this.state.person.yearsOld} y.o.</p>
                        <p>{this.state.person.location}</p>
                    </div>
                    }

                </div>
                {this.state.person.image &&
                <div className="img">
                    <img src={"data:" + this.state.person.image.contentType + ";base64, " + this.state.person.image.bytes}/>
                </div>
                }

            </header>

            <section className="section">
                <h2>Professional Summary</h2>
                <p>{this.state.person.summary}</p>
            </section>

            <section className="section">
                <h2>Experience</h2>
                <div className="experience" id="experience">

                    {this.state.employments.map(e =>
                        <div className="employment">
                            <div>
                                <h4>{e.employer.name}</h4>
                                <p>{formatDateToMonthAndYear(e.startDate)} - {e.endDate ? formatDateToMonthAndYear(e.endDate) : 'Now'} </p>
                            </div>


                            {e.careerSteps.map(c =>
                                <div>
                                    <span
                                        className="job-title">{c.title}</span>, {formatDateToMonthAndYear(c.startDate)}
                                    <ul>
                                        {c.assignments.map(a =>
                                            <li key={a.name}>
                                                {a.name} ({formatDateToMonthAndYear(a.startDate)} - {formatDateToMonthAndYear(a.endDate)})
                                                <p className="assignment-summary">{a.description}</p>
                                            </li>
                                        )}
                                    </ul>
                                </div>
                            )}

                        </div>
                    )}

                </div>
            </section>


            <section className="section" id="technologies">
                <a name="technologies"><h2 id="technologies">Skills / Technologies</h2></a>
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
                    <li>Cat Person</li>
                </ul>
            </section>
        </div>
    }
}