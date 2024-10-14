import {formatDateToMonthAndYear} from "../FormatDate";
import "./Assignment.css";

const Assignment = ({assignment}) => {
    return <div>
        {assignment.name} ({formatDateToMonthAndYear(assignment.startDate)} - {formatDateToMonthAndYear(assignment.endDate)})
        <div className="assignment-summary">
            {assignment.description}
        </div>
        <div className="duties">

            {assignment.duties.map((duty) => [
                <span>   {duty} <br/></span>
            ])}
        </div>

        <div className="assignment-summary">
            Tech:&nbsp;
            {assignment.technologies.map((tech, i) => [
                i > 0 && ", ",
                <span>{tech}</span>
            ])}
        </div>

        {assignment.url && <div><a href={assignment.url} target="_blank">See Details</a></div>}
    </div>
}

export default Assignment;