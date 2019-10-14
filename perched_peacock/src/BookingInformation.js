import React from 'react';
import { Component } from 'react';
import Form from 'react-bootstrap/Form';
import { Button, Col, Row } from 'react-bootstrap'
import { PostData } from './services/PostData';

class BookingInformation extends Component {

    state = {
        vehichleNumber: '',
        vehichleWeight: '',
        vehichleType: '',
        entryDateTime: '',
        customerFullName: '',
        contactNumber: '',

    };
    constructor(props) {
        super(props)
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    
    getDateTime() {

        let separator = '/';
        let newDate = new Date();
        let date = newDate.getDate();
        let month = newDate.getMonth() + 1;
        let year = newDate.getFullYear();
        let hours = new Date().getHours(); //Current Hours
        let min = new Date().getMinutes(); //Current Minutes

        return `${date}${separator}${month < 10 ? `0${month}` : `${month}`}${separator}${year}${' '}${hours}${':'}${min}`

    }

    updateValue(text, field) {

        console.warn(this.state);
        switch (field) {
            case "vehichleNumber":
                {
                    this.setDateTime();
                    this.setState({
                        vehichleNumber: text.currentTarget.value
                    })
                    break;
                }
            case "vehichleWeight":
                {
                    this.setDateTime();
                    this.setState({
                        vehichleWeight: text.currentTarget.value
                    })
                    break;
                }
            case "vehichleType":
                {
                    this.setDateTime();
                    this.setState({
                        vehichleType: text.currentTarget.value
                    })
                    break;
                }
            case "customerFullName":
                {
                    this.setDateTime();
                    this.setState({
                        customerFullName: text.currentTarget.value
                    })
                    break;
                }
            case "contactNumber":
                {
                    this.setDateTime();
                    this.setState({
                        contactNumber: text.currentTarget.value
                    })
                    break;
                }
            case "entryDateTime":
                {
                    this.setDateTime();
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
        collection.vehichleNumber = form.vehichleNumber.value;
        collection.vehichleWeight = form.vehichleWeight.value;
        collection.vehichleType = form.vehichleType.value;
        collection.customerFullName = form.customerFullName.value;
        collection.contactNumber = form.contactNumber.value;
        collection.entryDateTime = form.entryDateTime.value;
        let result = await PostData(collection, this.callback);

        console.warn('form submission data', result);
        console.warn('form submission data', collection);
    }

    callback(data) {
        console.warn(data);
    }
    
    setDateTime() {
        this.setState(
            {
                entryDateTime: this.getDateTime()
            }
        )
    }
    componentDidMount() {
        this.setDateTime();
    }

    render() {
        return (
            <Form inline onSubmit={(e) => this.handleSubmit(e)}>
                <Form.Row>
                    <Form.Group as={Col} controlId="vehichleNumber">
                        <Form.Label >Vehicle Number  </Form.Label>
                        <Form.Control required type="text" placeholder="Vehichle Number"
                            value={this.state.vehichleNumber}
                            onChange={(text) => this.updateValue(text, "vehichleNumber")}
                        />
                    </Form.Group>
                    <Form.Group as={Col} controlId='vehichleWeight'>
                        <Form.Label >Vehicle Weight  </Form.Label>
                        <Form.Control required type="text" placeholder="Vehichle Weight"
                            value={this.state.vehichleWeight}
                            onChange={(text) => this.updateValue(text, "vehichleWeight")}
                        />
                    </Form.Group>
                    <Form.Group as={Col} controlId="vehichleType">
                        <Form.Label>Vehicle Type</Form.Label>
                        <Form.Control as="select" value={this.state.vehichleType}
                            onChange={(text) => this.updateValue(text, "vehichleType")}>
                            <option>2W</option>
                            <option>4W</option>
                        </Form.Control>
                    </Form.Group>
                </Form.Row>
                <Form.Row>
                    <Form.Group as={Row} controlId="entryDateTime">
                        <Form.Label>Entry Date Time</Form.Label>
                        <Form.Control readOnly type="text" placeholder="Date Time"
                            value={this.state.entryDateTime}
                            onChange={(text) => this.updateValue(text, "entryDateTime")}
                        />
                        {/* <InputGroup>
                            <InputGroup.Prepend>
                                <InputGroup.Text id="inputGroupPrepend">{this.state.entryDateTime} </InputGroup.Text>
                            </InputGroup.Prepend>
                        </InputGroup>                       */}
                    </Form.Group>
                </Form.Row>
                <Form.Row>
                    <Form.Group as={Col} controlId="customerFullName">
                        <Form.Label>Full Name</Form.Label>
                        <Form.Control required type="text" placeholder="Full Name"
                            value={this.state.customerFullName}
                            onChange={(text) => this.updateValue(text, "customerFullName")}
                        />
                    </Form.Group>
                    <Form.Group as={Col} controlId='contactNumber'>
                        <Form.Label>Contact Number</Form.Label>
                        <Form.Control required type="text" placeholder="Contact Number"
                            value={this.state.contactNumber}
                            onChange={(text) => this.updateValue(text, "contactNumber")}
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

export default BookingInformation;