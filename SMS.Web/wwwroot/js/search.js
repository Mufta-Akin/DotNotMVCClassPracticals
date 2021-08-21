
import { html, LitElement } from 'https://unpkg.com/lit-element@latest/lit-element.js?module';

class LitTicket extends LitElement {
    // define state
    static get properties() {
        return {
            ticket: { type: Object }, 
        }
    }

    constructor() {
        super(); 
        this.ticket = { issue:'', resolution: '', student: ''};
    }

    // allow global styles
    createRenderRoot() {
        return this;
    }

    render() {
        console.log('rendering ticket', this.ticket);
        return html`
            <div class="card rounded shadow">
                <div class="card-body">
                    <h5 class="card-title">Ticket Details</h5>
                    <p class="card-text"><strong class="mr-2">Issue:</strong>${this.ticket.issue}</p>
                    <p class="card-text"><strong class="mr-2">Resolution:</strong>${this.ticket.resolution}</p>
                    <p class="card-text"><strong class="mr-2">Student:</strong>${this.ticket.student}</p>
                </div>
            </div>
        ` 
    }
}
customElements.define('lit-ticket',LitTicket);


// -------------------- Search Component -----------------
class LitSearch extends LitElement {
    // define state
    static get properties() {
        return { 
            query: { type: String },
            ticket: { type: Object },            
        }
    }
    
    // initialise properties
    constructor() {
        super(); 
        this.query = '';
        this.ticket = { issue: '', resolution: '', student: '' };
    }

    // allow global styles
    createRenderRoot() {
        return this;
    }
      
    // render html content
    render() {
        return html`
            <div class="my-4 d-flex justify-content-between align-items-center">  
                <form class="form-inline">
                    <input class="form-control mr-sm-2" placeholder="Search.."
                        value=${this.query} @change="${ this._updateQuery }">                       
                    <button class="btn btn-primary" type="submit" @click="${ this._getData }">Search</button>
                </form>
            </div>
            <lit-ticket .ticket=${this.ticket}></lit-ticket>           
        `;
    }

    // callback to update query property
    _updateQuery(e) { 
        this.query = e.target.value // update query using event target (input) value     
    }
    
    _reset() {
        this.query = '';
        this.ticket = {
            issue: '',
            resolution: '', 
            student: ''
        }
    }

    // load data from API
    _getData(e) {
        // prevent form from submitting an refreshing page
        e.preventDefault(); 

        fetch(`http://localhost:5000/api/tickets/id/${this.query}`)
            .then(resp => {               
                if (resp.ok) return resp.json()               
                throw new Error("invalid request")
            })
            .then(json => this.ticket = { 
                issue: json.issue,
                resolution: json.resolution, 
                student: json.studentName }
            ).catch( err => {
                console.log(err)
                this._reset()
            })      
    }
          

}
// define custom element named lit-search
customElements.define('lit-search', LitSearch);
