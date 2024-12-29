import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { BookService } from './Services/book.service';

@Component({
  selector: 'app-book',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss'], // Ensure the file exists
})
export class BookComponent implements OnInit {
  books: any[] = [];
  bookForm: FormGroup;

  constructor(private bookService: BookService) {
    // Initialize form group with required fields
    this.bookForm = new FormGroup({
      title: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      publishedYear: new FormControl('', Validators.required),
      author: new FormControl('', Validators.required),
    });
  }

  ngOnInit() {
    this.fetchBooks();
  }

  fetchBooks() {
    this.bookService.getAllBooks().subscribe(
      (response: any) => {
        if (response.success) {
          this.books = response.data;
        } else {
          console.error('Failed to fetch books:', response);
        }
      },
      (error) => {
        console.error('Error fetching books:', error);
      }
    );
  }

  addBook() {
    if (this.bookForm.valid) {
      console.log('Form Data:', this.bookForm.value); // Debugging
      this.bookService.addBook(this.bookForm.value).subscribe(
        (response: any) => {
          if (response.success) {
            alert(response.message);
            this.bookForm.reset();
           // this.fetchBooks();
          } else {
            alert('Failed to add book: ' + response.message);
          }
        },
        (error) => {
          console.error('Error adding book:', error);
        }
      );
    } else {
      alert('Please fill in all required fields.');
    }
  }
}
