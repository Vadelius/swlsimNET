import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HubConnection } from '@aspnet/signalr-client';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent implements OnInit {
    private hubConnection: HubConnection;
    public async: any;
    message = '';
    messages: string[] = [];

    constructor() {
    }

    public sendMessage(): void {
        const data = `Sent: ${this.message}`;

        this.hubConnection.invoke('Send', data);
        this.messages.push(data);
    }

    ngOnInit() {
        this.hubConnection = new HubConnection('/loopy');

        this.hubConnection.on('Send', (data: any) => {
            const received = `Received: ${data}`;
            this.messages.push(received);
        });

        this.hubConnection.start()
            .then(() => {
                console.log('Hub connection started')
            })
            .catch(err => {
                console.log('Error while establishing connection')
            });
    }

}
