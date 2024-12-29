import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';

interface Book {
  id: number,
  title: "",
  author: "",
  description: "",
  publishedYear: number
}

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private baseUrl = 'http://localhost:5025/api/book';

  constructor(private http: HttpClient) {}

  addBook(book: Book): Observable<Book> {
    return this.http.post<Book>(`${this.baseUrl}`, book,{ headers: { 'Content-Type': 'application/json' } }).pipe(
      catchError((error) => {
        console.error('Error adding book:', error);
        return throwError(error);
      })
    );
  }


  getAllBooks(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}`);
  }


}
