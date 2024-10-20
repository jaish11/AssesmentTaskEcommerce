import { Component } from '@angular/core';
import { RouterModule, Routes, RouterOutlet } from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';  // Ensure the path is correct
import { NavbarComponent } from './navbar/navbar.component';  // Assuming you have a Navbar component

// Define routes directly in the component
const routes: Routes = [
  { path: '', component: ProductListComponent },  // Default route to show ProductListComponent
  // Add other routes as needed here
];

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, NavbarComponent],  // Import RouterModule without .forRoot()
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'E-Commerce App';
}
