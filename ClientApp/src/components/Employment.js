import React from 'react';
import {formatDateToMonthAndYear} from "../FormatDate";
import CareerStep from "./CareerStep";
import "./Employment.css";

const Employment = ({employment}) => {
    return (
        <div className="employment">
            <div className="job-title-header">
                <h4>{employment.employer.name}</h4>
                <p>{formatDateToMonthAndYear(employment.startDate)} - {employment.endDate ? formatDateToMonthAndYear(employment.endDate) : 'Now'} </p>
            </div>
            
            {employment.careerSteps.map(c =>
                <CareerStep careerStep={c}/>
            )}

        </div>
    );
};

export default Employment;