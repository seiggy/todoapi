export * from './toDoItems.service';
import { ToDoItemsService } from './toDoItems.service';
export * from './toDoLists.service';
import { ToDoListsService } from './toDoLists.service';
export const APIS = [ToDoItemsService, ToDoListsService];
