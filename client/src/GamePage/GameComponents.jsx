import React, { Fragment } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText, Modal, ModalHeader, ModalBody, ModalFooter, Table } from 'reactstrap';

export class GameList extends React.Component {
    render() {
        const { games, onClick } = this.props;
        return (
            <Fragment>
                <Button onClick={() => onClick(null)}>New Game</Button>
                {games && games.items &&
                    <Table>
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Description</th>
                                <th>Difficulty</th>
                                <th>Number of Players</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {games.items.map(game =>
                                <GameRow key={game.id} onClick={onClick} game={game} ></GameRow>
                            )}
                        </tbody>
                    </Table >
                }
            </Fragment>
        )
    }
}

class GameRow extends React.Component {

    render() {
        const { game, onClick } = this.props;
        return (
            <tr>
                <td>
                    {game.name}
                </td>
                <td>
                    {game.description}
                </td>
                <td>
                    {game.difficulty}
                </td>
                <td>
                    {game.numberOfPlayers}
                </td>
                <td>
                    <Button onClick={() => onClick(game)}>Edit</Button>
                </td>
            </tr>
        )
    }
}

export class GameModal extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { handleChange, modalOpen, toggleModal, postGame } = this.props;

        return (
            <Modal isOpen={modalOpen} toggle={toggleModal}>
                <ModalHeader toggle={toggleModal}>{this.props.game.name}</ModalHeader>
                <ModalBody>
                    <Form>
                        <FormGroup>
                            <Label for="name">Name</Label>
                            <Input value={this.props.game.name} onChange={(e) => handleChange(e, "name")} type="text" valid />
                        </FormGroup>
                        <FormGroup>
                            <Label for="description">Description</Label>
                            <Input value={this.props.game.description} onChange={(e) => handleChange(e, "description")} type="textArea" valid />
                        </FormGroup>
                        <FormGroup>
                            <Label for="difficulty">Difficulty</Label>
                            <select value={this.props.game.difficulty} onChange={(e) => handleChange(e, "difficulty")}>
                                <option value="Easy">Easy</option>
                                <option value="Medium">Medium</option>
                                <option value="Hard">Hard</option>
                            </select>
                        </FormGroup>
                        <FormGroup>
                            <Label for="numberOfPlayers">Number of Players</Label>
                            <Input value={this.props.game.numberOfPlayers} onChange={(e) => handleChange(e, "numberOfPlayers")} type="number" valid />
                        </FormGroup>
                    </Form>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={() => postGame()}>
                        {this.props.game.id ? <div>Update Game</div> : <div>Create Game</div>}
                    </Button>{' '}
                    <Button color="secondary" onClick={toggleModal}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }
}