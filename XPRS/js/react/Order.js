import React from "react";
import ReactDOM from "react-dom";
import rd3 from "react-d3";
import Bootstrap, { Grid, Row, Col, Table, Panel, PanelHeading, PanelBody, Navbar, Nav, NavItem, NavDropdown, MenuItem } from "react-bootstrap";
import axios from "axios";

const baseUrl = 'http://localhost:44291';


const Dashboard = ({ orders }) => (
    <Grid>
        <Row>
            <Col xs={2}></Col>
            <Col xs={8}>
                <OrderList orders={orders} />
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

const OrderList = ({ orders }) => (
    <div>
    {orders.map((order, index) =>
        <OrderPanel
            order={order}
            key={index} />
        )}
    </div>
    
);

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
    }

    render() {
        return (
            <Panel key={this.state.order.OrderID} bsStyle="primary" defaultExpanded>
                <Panel.Heading>
                    <Panel.Title toggle componentClass="h3">
                        {this.state.order.ContractNumber}
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
                    <Dashboard orders={this.state.Orders} />
                </div>
            );
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



ReactDOM.render(<App />, document.getElementById("app"));