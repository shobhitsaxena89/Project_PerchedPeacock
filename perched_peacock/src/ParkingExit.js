import React from 'react';
import { Component } from 'react';
import Form from 'react-bootstrap/Form';
import { Button, Col } from 'react-bootstrap'
import { GetData } from './services/GetData';

class ParkingExit extends Component {

    state = {
        amount: '',
        duration: '',
        entryDateTime: '',
        exitDateTime: '',

    };

    constructor(props) {
        super(props)
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    updateValue(text, field) {

        console.warn(this.state);
        switch (field) {

            case "exitDateTime":
                {

                    this.setState({
                        exitDateTime: text.currentTarget.value
                    })
                    break;
                }
            case "entryDateTime":
                {
                    this.setState({
                        contactNumber: text.currentTarget.value
                    })
                    break;
                }
            case "amount":
                {
                    this.setState({
                        amount: text.currentTarget.value
                    })
                    break;
                }
            case "duration":
                {
                    this.setState({
                        duration: text.currentTarget.value
                    })
                    break;
                }
            default:
                console.error("Invalid Form Data");
                break;
        }
    }

    async handleSubmit(event) {
        let form = event.currentTarget;
        console.warn(form.value);
        let collection = {}
        collection.amount = form.amount.value;
        collection.duration = form.duration.value;
        collection.entryDateTime = form.entryDateTime.value;
        collection.exitDateTime = form.exitDateTime.value;
        let data = await GetData(form.parkingId.value);
        this.setState({
            amount: data.amount
        })
        this.setState({
            duration: data.duration
        })
        this.setState({
            entryDateTime: data.entryDateTime
        })
        this.setState({
            exitDateTime: data.exitDateTime
        })
        console.warn('form submission data', collection);
    }

    render() {
        return (
            <Form inline onSubmit={(e) => this.handleSubmit(e)}>

                <Form.Row>
                    <Form.Group as={Col} controlId="parkingId">
                        <Form.Label >Parking ID  </Form.Label>
                        <Form.Control required type="text" placeholder="Enter Parking Id"
                            value={this.state.parkingId}
                            onChange={(text) => this.updateValue(text, "parkingId")}
                        />
                    </Form.Group>
                </Form.Row>
                <Form.Row>
                    <Form.Group as={Col} controlId="entryDateTime">
                        <Form.Label>Entry Date Time </Form.Label>
                        <Form.Control readOnly type="text" placeholder=""
                            value={this.state.entryDateTime}
                            onChange={(text) => this.updateValue(text, "entryDateTime")}
                        />
                    </Form.Group>
                    <Form.Group as={Col} controlId="exitDateTime">
                        <Form.Label>Exit Date Time </Form.Label>
                        <Form.Control readOnly type="text" placeholder=""
                            value={this.state.exitDateTime}
                            onChange={(text) => this.updateValue(text, "exitDateTime")}
                        />
                    </Form.Group>               
                    <Form.Group as={Col} controlId="amount">
                        <Form.Label>Amount Payable </Form.Label>
                        <Form.Control readOnly type="text" placeholder=""
                            value={this.state.amount}
                            onChange={(text) => this.updateValue(text, "amount")}
                        />
                    </Form.Group>
                    <Form.Group as={Col} controlId="duration">
                        <Form.Label>Parking Duration </Form.Label>
                        <Form.Control readOnly type="text" placeholder=""
                            value={this.state.duration}
                            onChange={(text) => this.updateValue(text, "duration")}
                        />
                    </Form.Group>
                </Form.Row>

                <Form.Row>
                    <Button type="submit"


                        id="blog_post_submit"
                        className="btn-default btn"
                        align="center"
                    >
                        Submit
                            </Button>
                </Form.Row>
            </Form>
        );
    }
}

export default ParkingExit;