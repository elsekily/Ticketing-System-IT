import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Ticket } from '../ticket-form/ticket-form.component';

@Injectable({
    providedIn: 'root'
})

export class TicketService {

    constructor(private http: HttpClient) { }


    create(ticket: Ticket) {
        return this.http.post<any>('/api/ticket/create', ticket);
    }
}
