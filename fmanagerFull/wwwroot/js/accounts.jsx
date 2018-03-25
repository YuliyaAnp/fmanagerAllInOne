class Accounts extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            accounts: this.props.initialData
        };
        this.getAccountsFromServer = this.getAccountsFromServer.bind(this);
    }

    getAccountsFromServer()
    {
        console.log("getAccountFromServer");
        fetch(this.props.url, {
                        mode: 'cors',
                        cache: 'no-cache',      
                        headers : { 
                                'content-type': 'application/json',
                                'accept': 'application/json'
                              }})
         .then(response => response.json())
         .then(json => {
         console.log(json);
         this.setState({ accounts: json});
         });
    }

    render() {
      return (
          <div>
              <div>
                <TableOfAccounts accounts={this.state.accounts}/>
            </div>
          </div>
          );
  }
};

class TableOfAccounts extends React.Component {
    constructor(props){
        super(props);
    }

    render() {
    var cols = [
    { key: 'id', label: 'No'},
    { key: 'balance', label: 'Balance' },
    { key: 'name', label: 'Name' },
    { key: 'currency', label: 'Currency' },
    { key: 'type', label: 'Type' }
    ];

        return (
            <div>
                <h1>Accounts</h1>
                <table>
                    <thead>
                    <tr>
                        <th style={{"width" : "10%"}}>{cols[0].label}</th>
                        <th style={{"width" : "20%"}}>{cols[1].label}</th>
                        <th style={{"width" : "20%"}}>{cols[2].label}</th>
                        <th style={{"width" : "20%"}}>{cols[3].label}</th>
                        <th style={{"width" : "20%"}}>{cols[4].label}</th>
                    </tr>
                    </thead>
                    <tbody>
                      {this.props.accounts.map(function(item) 
                        {
                            var cells = cols.map(function(colData) 
                            {
                                return <td>{item[colData.key]}</td>;
                            });
                            return <tr key={item.id}>{cells}<td>
                            </td></tr>;
                        })} 
                    </tbody>
                </table>
            </div>
        );
    }
};