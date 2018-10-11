import React, { Fragment } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText, Modal, ModalHeader, ModalBody, ModalFooter, Table } from 'reactstrap';
import { Link } from 'react-router-dom'

export class EventList extends React.Component {
    render() {
        const { events, onClick } = this.props;
        return (
            <Fragment>
                <Button onClick={() => onClick(null)}>New Event</Button>
                {events && events.items && events.items.length &&
                    <Table>
                        <thead>
                            <tr>
                                <th>Name</th>
                            </tr>
                        </thead>
                        <tbody>
                            {events.items.map(event =>
                                <EventRow key={event.id} onClick={onClick} event={event} ></EventRow>
                            )}
                        </tbody>
                    </Table >
                }
            </Fragment>
        )
    }
}

class EventRow extends React.Component {

    render() {
        const { event, onClick } = this.props;
        return (
            <tr>
                <td>
                    <Link to={"ManageEvent/" + event.id} props = {event.id}>{event.name}</Link>
                </td>
                <td>
                    <Button onClick={() => onClick(event)}>Edit</Button>
                </td>
            </tr>
        )
    }
}

export class EventModal extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { event, handleChange, modalOpen, toggleModal, postEvent } = this.props;

        return (
            <Modal isOpen={modalOpen} toggle={toggleModal}>
                <ModalHeader toggle={toggleModal}>{event.name}</ModalHeader>
                <ModalBody>
                    <Form>
                        <FormGroup>
                            <Label for="name">Name</Label>
                            <Input name="name" value={event.name || ''} onChange={(e) => handleChange(e)} type="text" valid />
                        </FormGroup>
                    </Form>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={() => postEvent()}>
                        {event.id ? <div>Update Event</div> : <div>Create Event</div>}
                    </Button>{' '}
                    <Button color="secondary" onClick={toggleModal}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }
}