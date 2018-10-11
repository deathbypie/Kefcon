import React from 'react';
import { TimeslotList } from './ManageEventComponents';
import { eventService } from './../_services';

export class ManageEventPage extends React.Component {

    componentDidMount() {
        eventService.getById(this.props.match.params.id).then((result) => {
            this.setState((prevState, props) => ({
                event: { loading: false, data: result }
            }));
        });
    }

    constructor(props) {
        super(props);

        this.state = {
            event: { loading: true }
        };
    }

    render() {
        const { event } = this.state
        return (
            <div>
                {event.error && <span className="text-danger">ERROR: {event.error}</span>}
                {event.loading && <em>Loading... <br/></em>}
                    {event.data && 
                        <div>
                            <h2>{event.data.name}</h2>
                            <TimeslotList event={event} onClick={(timeslot) => this.fillModal(timeslot)}></TimeslotList>
                        </div>
                    }
            </div>
        )
    }
}