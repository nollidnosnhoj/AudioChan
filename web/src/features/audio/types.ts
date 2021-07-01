import { MetaAuthor } from "~/lib/types";

export enum Visibility {
  Public = "public",
  Unlisted = "unlisted",
  Private = "private",
}

export interface AudioData {
  id: number;
  title: string;
  visibility: Visibility;
  duration: number;
  picture?: string;
  uploaded: string;
  audioUrl: string;
  user: MetaAuthor;
  privateKey?: string;
}

export interface AudioDetailData extends AudioData {
  description?: string;
  tags: string[];
  fileSize: number;
  uploaded: string;
  lastModified?: string;
}

export interface AudioRequest {
  title: string;
  description?: string;
  tags: string[];
  visibility: Visibility;
}

export interface CreateAudioRequest extends AudioRequest {
  uploadId: string;
  fileName: string;
  fileSize: number;
  duration: number;
}
