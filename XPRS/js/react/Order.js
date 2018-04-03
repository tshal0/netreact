import React from "react";
import ReactDOM from "react-dom";
import rd3 from "react-d3";
import Bootstrap, { Grid, Row, Col, Table, Panel, PanelHeading, PanelBody, Navbar, Nav, NavItem, NavDropdown, MenuItem, Button, Glyphicon, Image} from "react-bootstrap";
import axios from "axios";

const baseUrl = 'http://localhost:44291';


const Dashboard = ({ orders, getOrders, removeOrder }) => (
    <Grid>
        <Row>
            <Col xs={2}></Col>
            <Col xs={8}>
                <OrderList orders={orders} getOrders={getOrders} removeOrder={removeOrder}/>
            </Col>
            <Col xs={2}></Col>
        </Row>
    </Grid> 
);

const MainNavBar = ({ }) => (
    <Navbar fixedTop>
        <Navbar.Header>
            <Navbar.Brand>
                <a href="#brand">Simtech XPRS</a>
            </Navbar.Brand>
        </Navbar.Header>
    </Navbar>
);

class OrderList extends React.Component {
    constructor(props, context) {
        super(props, context);
        console.log("OrderList CONSTRUCTOR")
        console.log(props);
        this.state = {
            orders: props.orders,
            open: true
        };
        console.log(this.state.orders);
        this.getOrders = props.getOrders.bind(this);
        
    }

    getOrders() {
        console.log("OrderList");
        this.props.getOrders();
        this.forceUpdate();
    }

    removeOrder(orderID) {
        axios.delete(`${baseUrl}/api/order/${orderID}`)
            .then(response => {
                this.setState({
                    orders: response.data
                })
                this.forceUpdate();
            });
    }

    render() {
        return (
            <div>
                {this.state.orders.map((order, index) =>
                    <OrderPanel
                        order={order}
                        key={index}
                        getOrders={this.getOrders}
                        removeOrder={this.removeOrder}
                    />
                    )}
            </div>

        );
    }
}


const Order = ({ order }) => (
    
    <Panel key={order.OrderID} bsStyle="primary">
        <Panel.Heading>
            <Panel.Title componentClass="h3">
                {order.ContractNumber}
            </Panel.Title>
        </Panel.Heading>
        <Panel.Body>
            {order.Placements.map((placement, index) =>
                <Placement
                    placement={placement}
                    key={index} />
            )}
        </Panel.Body>
    </Panel>

    
);

class OrderPanel extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            order: props.order,
            open: true
        };

        this.getOrders = props.getOrders.bind(this);
        this.removeOrder = props.removeOrder.bind(this);
    }

    handleClose() {
        
    }

    getOrders() {
        console.log("OrderPanel");
        this.props.getOrders();
    }

    removeOrder(orderID) {
        this.props.removeOrder(orderID);
        this.forceUpdate();
    }

    render() {
        return (
            <Panel key={this.state.order.OrderID} bsStyle="primary" defaultExpanded>
                <Panel.Heading>
                    <Panel.Title toggle componentClass="h3">
                        {this.state.order.ContractNumber}
                        <Image src="/Content/img/grey_close.png" onClick={() => this.removeOrder(this.state.order.OrderID)} style={{float:'right'}}/>
                    </Panel.Title>
                    
                </Panel.Heading>
                <Panel.Collapse>
                    <Panel.Body>
                        {this.state.order.Placements.map((placement, index) =>
                            <Placement
                                placement={placement}
                                key={index} />
                        )}
                    </Panel.Body>
                </Panel.Collapse>
            </Panel>
        );
    }
}

const Placement = ({ placement }) => (
    <Panel key={placement.ContractorID} >
        <Panel.Heading>{placement.Contractor.Name}</Panel.Heading>
        <Panel.Body></Panel.Body>
    </Panel>
);

const App = React.createClass({

    getInitialState() {
        return {
            //Orders: [{ "ContractNumber": "loading...", "Placements": [{ "ContractorID": "Loading...", "Contractor": {"Name": "Loading..."} }]}]
        }
    },

    getOrders() {
        console.log("App");
        axios.get(`${baseUrl}/api/order`)
            .then(response => {
                this.setState({
                    Orders: response.data
                })
            });
    },

    removeOrder(orderID) {
        axios.delete(`${baseUrl}/api/order/${orderID}`)
            .then(response => {
                this.setState({
                    Orders: response.data
                })
                this.forceUpdate();
            });
    },

    render() {
        console.log("render APP");
        if (this.state.Orders === undefined) {
            return (
                <div>
                    <MainNavBar />
                    Loading
                </div>
            );
        } else {
            console.log(this.state.Orders);
            return (
                <div>
                    <MainNavBar/>
                    <Dashboard orders={this.state.Orders} getOrders={this.getOrders} removeOrder={this.removeOrder} />
                </div>
            );
        }
    },

    componentDidMount() {
        console.log("API call");
        this.getOrders();
    },
});



ReactDOM.render(<App />, document.getElementById("app"));