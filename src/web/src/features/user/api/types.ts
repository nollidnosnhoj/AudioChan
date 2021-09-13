import { ID } from "~/lib/types";

export type CurrentUser = {
  id: ID;
  userName: string;
  email: string;
};

export type Profile = {
  id: ID;
  userName: string;
  picture: string;
};
