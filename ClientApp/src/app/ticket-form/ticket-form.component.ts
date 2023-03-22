import { CommonModule } from '@angular/common';
import { Component, OnInit, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CategoryService } from '../services/category.service';
import { TicketService } from '../services/TicketService';

@Component({
  selector: 'app-ticket-form',
  templateUrl: './ticket-form.component.html',
  styleUrls: ['./ticket-form.component.css']
})
export class TicketFormComponent implements OnInit {
  categories: Category[];
  ticket: Ticket;
  constructor(private categorySerivce: CategoryService, private ticketService: TicketService) {
    this.categories = [];
    this.ticket = {
      categoryId: 0,
      Description: '',
    }

  }

  ngOnInit(): void {
    this.categorySerivce.getCategories().subscribe(result => {
      this.categories = result;
      console.log(this.categories);
    }, error => console.error(error))
  }
  submit() {
    console.log(this.ticket);
    this.ticketService.create(this.ticket).subscribe();


  }

}
export interface Category {
  id: number;
  name: string;
  estimatedTimeInMinutes: number;
}
export interface Ticket {
  Description: string;
  categoryId: number;
}