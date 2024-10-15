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

        {assignment.link && <div><a href={assignment.link.address} target="_blank"> {assignment.link.caption || <span>See Details</span>}</a></div>}
    </div>
}

export default Assignment;