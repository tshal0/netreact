import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";

const baseUrl = 'http://localhost:44291';

const Welcome = ({ onSubmit }) => {
    let usernameInput;
    return (
        <div>
            <p>Enter your Twitter name and start chatting!</p>
            <form onSubmit={(e) => {
                e.preventDefault();
                onSubmit(usernameInput.value);
            }}>
                <input type="text" placeholder="Enter Twitter handle here" ref={node => {
                    usernameInput = node;
                }} />
                <input type="submit" value="Join the chat" />
            </form>
        </div>
    );
};



const OrderList = ({ orders }) => (
    <ul>
        {orders.map((order, index) =>
            <Order
                order={order}
                key={index} />
        )}
    </ul>
);

const Order = ({ order }) => (
    <li key={order.OrderID} className='order-li'>
        <strong>{order.ContractNumber}</strong>
        <ul>
            {order.Placements.map((placement, index) =>
                <Placement
                    placement={placement}
                    key={index} />
            )}
        </ul>
    </li>
);

const Placement = ({ placement }) => (
    <li key={placement.ContractorID} >
        <strong>{placement.Contractor.Name}</strong>
    </li>
);

const App = React.createClass({

    getInitialState() {
        return {
            Orders: [{ "ContractNumber": "loading...", "Placements": [{ "ContractorID": "Loading...", "Contractor": {"Name": "Loading..."} }]}]
        }
    },

    render() {
        console.log("render APP");
        if (this.state.Orders === null) {
            return (
                <div>Something went wrong</div>
            );
        } else {
            console.log(this.state.Orders);
            return <OrderList orders={this.state.Orders} />;
        }
    },

    componentDidMount() {
        console.log("API call");
        axios.get(`${baseUrl}/api/order`)
            .then(response => {
                this.setState({
                    Orders: response.data
                })
            })
    },
});

const getOrders = 

ReactDOM.render(<App />, document.getElementById("app"));