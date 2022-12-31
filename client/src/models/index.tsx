export type FileEntity = {
  guid: string;
  fileType: string;
  name: string;
};

export type User = {
  id: number;
  username: string;
  email: string;
  avatar?: FileEntity;
  roles: string[];
};

export type Profile = {
  id: number;
  name?: string;
  address: {
    city?: string;
    houseNumber?: string;
    postalCode?: string;
    region?: string;
    state?: string;
    street?: string;
  };
  createdAt: string;
  dateOfBirth?: string;
  phoneNumber?: string;
  sex?: "Male" | "Female" | "Other";
  user: User;
};

export type Photo = {
  id: number;
  title: string;
  description: string;
  fileEntity: FileEntity;
};

export type Gallery = {
  id: number;
  title: string;
  description: string;
  photos: Photo[];
};

export type Post = {
  id: number;
  title: string;
  content: string;
  createdAt: string;
  userId: number;
  user: User;
};

export type Paginated<TItem> = {
  page: number;
  size: number;
  items: TItem[];
};

export type Comment = {
  id: number;
  userId: number;
  user: User;
  content: string;
  createdAt: string;
};

export type SearchResult = {
  type: "User" | "Event" | "Group";
  id: number;
  name: string;
  description?: string;
  image?: FileEntity;
  createdAt?: string;
};

export type Message = {
  id: number;
  receiverId: number;
  receiver: User;
  authorId: number;
  author: User;
  content: string;
  createdAt: string;
  attachmentId?: number;
};

export type Group = {
  id: string;
  name: string;
  description: string;
  groupMembers: {
    user: User
  }[]
};
