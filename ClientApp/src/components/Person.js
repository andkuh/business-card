import "./Person.css";

const Person = ({person}) =>{
    return <div className="personal">
        <div>
            {person.id &&
            <div >
                <h1>{person.firstName} {person.lastName}</h1>
                <p>{person.specialization}, {person.yearsOld} y.o.</p>
                <p>{person.location}</p>
            </div>
            }

        </div>
        {person.image &&
        <div className="img">
            <img src={"data:" + person.image.contentType + ";base64, " + person.image.bytes} alt={"img"}/>
        </div>
        }
    </div>
}

export default Person;