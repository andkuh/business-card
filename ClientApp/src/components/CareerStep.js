import React from 'react';
import {formatDateToMonthAndYear} from "../FormatDate";
import Assignment from "./Assignment";
import "./CareerStep.css";

const CareerStep = ({careerStep}) => {
    return <div>
          <span
              className="job-title">{careerStep.title}</span>, {formatDateToMonthAndYear(careerStep.startDate)}
        <div className="projects">
            Projects:
        </div>
        <ul>
            {careerStep.assignments.map(a =>
                <li key={a.name}>
                    <Assignment assignment={a}/>
                </li>
            )}
        </ul>
    </div>

}

export default CareerStep;