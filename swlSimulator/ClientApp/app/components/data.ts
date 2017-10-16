export class Data {
    status: any;
    response:string;
    constructor(json: any) {
        this.status = json.status;
    }
}