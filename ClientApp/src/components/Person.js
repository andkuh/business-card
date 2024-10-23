import "./Person.css";

const Person = ({person, links}) => {
    return <div className="personal">
        <div>
            {person.id &&
            <div>
                <div>
                    <h1>{person.firstName} {person.lastName}</h1>
                    <p>{person.specialization}, {person.yearsOld} y.o.</p>
                    <p>{person.location}</p>
                </div>
                <div>
                    <ul>
                        {links && links.map(s =>

                            <div className={s.type.toLowerCase()}>
                                <li key={s.value} >
                                    <a href={s.type !== 'Email' ? s.value : 'mailto:' + s.value}>{s.type}</a>
                                </li>
                            </div>
                        )}
                    </ul>
                </div>
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