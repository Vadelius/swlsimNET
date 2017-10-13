import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Configuration } from './app/app.constants'
import { HubConnection } from '@aspnet/signalr-client';
import { SettingsService } from "./settings.service";
import { Settings } from "./settings.interface";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent implements OnInit {
    private hubConnection: HubConnection;
    public async: any;
    message = '';
    messages: string[] = [];


    constructor(
        private _settingsservice: SettingsService,
        private _settings: Configuration) {
    }
    public settings: Settings[];

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

        this._settingsservice
            .getAll()
            .subscribe((data: Settings[]) => this.settings = data,
                error => console.log(error),
                () => console.log("getAllItems() complete from init"));
    }

}
