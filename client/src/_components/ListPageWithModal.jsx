// import React from 'react';

// export function ListPageWithModal(PageComponent, ModalComponent) {
//     return class extends React.Component {
//         constructor(props) {
//             super(props);
    
//             this.state = {
//                 activeModel: {},
//                 modalOpen: false
//             };
//         }

//         toggleModal() {
//             this.setState({
//                 modalOpen: !this.state.modalOpen
//             });
//         }
    
//         fillModal(model) {
//             this.setState({
//                 activeModel: { ...model },
//                 modalOpen: !this.state.modalOpen
//             });
//         }

//         handleChange(e) {
//             const target = e.target;
//             const value = target.value;
//             const model = this.state.activeModel;
    
//             this.setState({
//                 activeModel: { ...model, [target.name]: value }
//             });
//         }

//         render() {
//             return (
//                 <div>
//                     <PageComponent />>
//                     <div>
//                         {this.state.activeModel &&
//                             <ModalComponent>
//                                 game={this.state.activeModel}
//                                 postGame={() => this.postGame()}
//                                 toggleModal={() => this.toggleModal()}
//                                 handleChange={(e, type) => this.handleChange(e, type)}
//                                 {...this.state} >
//                             </ModalComponent>
//                         }
//                     </div>
//                 </div>
//             )
//         }
//     };
// }