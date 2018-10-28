import React, { Fragment } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText, Modal, ModalHeader, ModalBody, ModalFooter, Table } from 'reactstrap';

export class TimeslotList extends React.Component {
    render() {
        const { event, onClick } = this.props;
        return (
            <Fragment>
                <Button onClick={() => onClick(null)}>New Timeslot</Button>
                {event && event.data && event.data.timeslots && event.data.timeslots.length != 0 &&
                    <Table>
                        <thead>
                            <tr>
                                <th>Timeslot</th>
                            </tr>
                        </thead>
                        <tbody>
                            {event.data.timelots.map(timeslot =>
                                <TimeslotRow key={timeslot.id} onClick={onClick} timeslot={timeslot} ></TimeslotRow>
                            )}
                        </tbody>
                    </Table >
                }
            </Fragment>
        )
    }
}

class TimeslotRow extends React.Component {

    render() {
        const { timeslot, onClick } = this.props;
        return (
            <tr>
                <td>
                    {timeslot.startTime} - {timeslot.endTime}
                </td>
                <td>
                    <Button onClick={() => onClick(event)}>Edit</Button>
                </td>
            </tr>
        )
    }
}