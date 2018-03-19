import React from 'react';
import { gameActions } from '../_actions';
import { connect } from 'react-redux';
import { GameList, GameModal } from './GameComponents';

class GamePage extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            activeGame: {},
            modalOpen: false
        };
    }

    componentWillMount() {
        this.props.dispatch(gameActions.getAll());
    }

    toggleModal() {
        this.setState({
            modalOpen: !this.state.modalOpen
        });
    }

    fillModal(game) {
        this.setState({
            activeGame: { ...game },
            modalOpen: !this.state.modalOpen
        });
    }

    postGame() {
        const game = this.state.activeGame;
        if (game != null && game.id != null) {
            this.props.dispatch(gameActions.update(game)).then(() => {
                this.toggleModal();
            });
        }
        else {
            this.props.dispatch(gameActions.create(game)).then(() => {
                this.toggleModal();
            });
        }
    }

    handleChange(e, type) {
        const value = e.target.value;
        const game = this.state.activeGame;

        switch (type) {
            case "name":
                this.setState({
                    activeGame: { ...game, name: value }
                });
                break;
            case "description":
                this.setState({
                    activeGame: { ...game, description: value }
                });
                break;
            case "difficulty":
                this.setState({
                    activeGame: { ...game, difficulty: value }
                });
                break;
            case "numberOfPlayers":
                this.setState({
                    activeGame: { ...game, numberOfPlayers: value }
                });
                break;
        }
    }

    render() {
        const { games } = this.props;

        return (
            <div>
                {games.loading && <em>Loading games...</em>}
                {games.error && <span className="text-danger">ERROR: {games.error}</span>}
                <div>
                    {games.items &&
                        <GameList games={games} onClick={(game) => this.fillModal(game)}></GameList>
                    }
                    {this.state.activeGame &&
                        <GameModal
                            game={this.state.activeGame}
                            postGame={() => this.postGame()}
                            toggleModal={() => this.toggleModal()}
                            handleChange={(e, type) => this.handleChange(e, type)}
                            {...this.state} >
                        </GameModal>
                    }
                </div>
            </div>
        )
    }
}

function mapStateToProps(state) {
    const { games } = state;
    return {
        games
    };
}

const connectedGamePage = connect(mapStateToProps)(GamePage);
export { connectedGamePage as GamePage };