import React from 'react';
import { EventList, EventModal } from './EventComponents';
import { eventService } from './../_services';

export class EventPage extends React.Component {

    componentDidMount() {
        eventService.getAll().then((result) => {
            this.setState((prevState, props) => ({
                events: { loading: false, items: result }
            }));
        });
    }

    constructor(props) {
        super(props);

        this.state = {
            activeEvent: {},
            events: { loading: true },
            modalOpen: false
        };
    }

    toggleModal() {
        this.setState({
            modalOpen: !this.state.modalOpen
        });
    }

    fillModal(event) {
        this.setState({
            activeEvent: { ...event },
            modalOpen: !this.state.modalOpen
        });
    }

    postEvent() {
        const updatedEvent = this.state.activeEvent;
        if (updatedEvent != null && updatedEvent.id != null) {
            eventService.update(updatedEvent).then(() => {
                const updatedItems = this.state.events.items.map((event) => {
                    if (updatedEvent.id === event.id) {

                        const eventCopy = { ...updatedEvent };
                        return eventCopy;
                    }
                    return event;
                });
                this.setState((prevState, props) => ({
                    events: { ...prevState.events, items: updatedItems }
                }));
                this.toggleModal();
            },
                error => {
                    this.setState((prevState, props) => ({
                        events: { ...prevState.events, error: "Failed to update event" }
                    }));
                }
            );
        }
        else {
            eventService.create(updatedEvent).then(() => {
                const updatedItems = [...this.state.events.items, updatedEvent];
                this.setState((prevState, props) => ({
                    events: { ...prevState.events, items: updatedItems }
                }));
                this.toggleModal();
            });
            error => {
                this.events.error = "Failed to create event";
            }
        }
    }

    handleChange(e) {
        const target = e.target;
        const value = target.value;
        const event = this.state.activeEvent;

        this.setState({
            activeEvent: { ...event, [target.name]: value }
        });
    }

    render() {
        const { events } = this.state;

        return (
            <div>
                {events.error && <span className="text-danger">ERROR: {events.error}</span>}
                <div>
                    {events.loading && <em>Loading events... <br/></em>}
                    <EventList events={events} onClick={(event) => this.fillModal(event)}></EventList>
                    {this.state.activeEvent &&
                        <EventModal
                            event={this.state.activeEvent}
                            postEvent={() => this.postEvent()}
                            modalOpen={this.state.modalOpen}
                            toggleModal={() => this.toggleModal()}
                            handleChange={(e, type) => this.handleChange(e, type)}>
                        </EventModal>
                    }
                </div>
            </div>
        )
    }
}